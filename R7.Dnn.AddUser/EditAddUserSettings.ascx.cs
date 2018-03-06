using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.AddUser.Models;
using R7.Dnn.Extensions.Modules;
using R7.Dnn.Extensions.Utilities;

namespace R7.Dnn.AddUser
{
    public class EditAddUserSettings : ModuleSettingsBase<AddUserSettings>
    {
        #region Controls

        protected Panel panelGeneralSettings;
        protected TextBox textDisplayNameFormat;
        protected TextBox textUserNameFormat;
        protected TextBox textDesiredPasswordLength;
        protected TextBox textAllowedSpecialChars;
        protected TextBox textRoles;
        protected TextBox textDoneUrl;
        protected CheckBox checkDoneUrlOpenInPopup;

        #endregion

        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            if (panelGeneralSettings != null) {
                panelGeneralSettings.Visible = UserInfo.IsSuperUser || UserInfo.IsInRole ("Administrators");
            }
        }

        #region ModuleSettingsBase overrides

        public override void LoadSettings ()
        {
            try {
                if (!IsPostBack) {
                    textDisplayNameFormat.Text = Settings.DisplayNameFormat;
                    textUserNameFormat.Text = Settings.UserNameFormat;
                    textDesiredPasswordLength.Text = Settings.DesiredPasswordLength.ToString ();
                    textAllowedSpecialChars.Text = Settings.AllowedSpecialChars;
                    textDoneUrl.Text = Settings.DoneUrl;
                    checkDoneUrlOpenInPopup.Checked = Settings.DoneUrlOpenInPopup;
                    textRoles.Text = string.Join (
                        ", ",
                        ParseRoleIdsStringToRoleNames (Settings.Roles, PortalId)
                            .Select (roleName => roleName.Trim ())
                    );
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        public override void UpdateSettings ()
        {
            try {
                Settings.DisplayNameFormat = textDisplayNameFormat.Text.Trim ();
                Settings.UserNameFormat = textUserNameFormat.Text.Trim ();
                Settings.DesiredPasswordLength = TypeUtils.ParseToNullable<int> (textDesiredPasswordLength.Text);
                Settings.AllowedSpecialChars = textAllowedSpecialChars.Text.Trim ();
                Settings.DoneUrl = textDoneUrl.Text.Trim ();
                Settings.DoneUrlOpenInPopup = checkDoneUrlOpenInPopup.Checked;
                Settings.Roles = string.Join (
                    ";",
                    ParseRoleNamesStringToRoleIds (textRoles.Text, PortalId)
                        .Select (roleId => roleId.ToString ())
                );

                SettingsRepository.SaveSettings (ModuleConfiguration, Settings);
                ModuleController.SynchronizeModule (ModuleId);
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        #endregion

        IEnumerable<string> ParseRoleIdsStringToRoleNames (string roleIds, int portalId)
        {
            foreach (var strRoleId in (roleIds ?? string.Empty)
                     .Split (";".ToCharArray (), StringSplitOptions.RemoveEmptyEntries)) {
                var role = RoleController.Instance.GetRoleById (portalId, int.Parse (strRoleId));
                if (role != null) {
                    yield return role.RoleName;
                }
            }
        }

        IEnumerable<int> ParseRoleNamesStringToRoleIds (string roleNames, int portalId)
        {
            foreach (var roleName in (roleNames ?? string.Empty)
                     .Split (",;".ToCharArray (), StringSplitOptions.RemoveEmptyEntries)) {
                var role = RoleController.Instance.GetRoleByName (portalId, roleName.Trim ());
                if (role != null) {
                    yield return role.RoleID;
                }
            }
        }
    }
}

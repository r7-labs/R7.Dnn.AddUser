//
// EditAddUserSettings.ascx.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2018 Roman M. Yagodin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.AddUser.Models;
using R7.Dnn.Extensions.Modules;

namespace R7.Dnn.AddUser
{
    public class EditAddUserSettings : ModuleSettingsBase<AddUserSettings>
    {
        #region Controls

        protected Panel panelGeneralSettings;
        protected TextBox textDisplayNameFormat;
        protected TextBox textUserNameFormat;
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

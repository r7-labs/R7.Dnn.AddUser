using System;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Security.Membership;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.AddUser.Components;
using R7.Dnn.AddUser.Models;
using R7.Dnn.Extensions.ModuleExtensions;
using R7.Dnn.Extensions.Modules;

namespace R7.Dnn.AddUser
{
    public class AddUser : PortalModuleBase<AddUserSettings>
    {
        #region Controls

        protected LinkButton buttonAddUser;
        protected HyperLink linkCancel;
        protected TextBox textFirstName;
        protected TextBox textLastName;
        protected TextBox textOtherName;
        protected TextBox textEmail;
        protected TextBox textPassword;
        protected TextBox textLogin;
        protected Panel panelUserInfo;
        protected Panel panelUserAdded;
        protected HyperLink linkDone;

        #endregion

        #region Handlers

        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            // set url for Cancel link
            linkCancel.NavigateUrl = Globals.NavigateURL ();
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);

            try {
                if (!IsPostBack) {
                }
            } catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        protected void buttonAddUser_Click (object sender, EventArgs e)
        {
            try {
                if (Page.IsValid) {
                    var addUserManager = new AddUserManager (
                        new NameFormatter (),
                        new PasswordGenerator ()
                    );

                    var addUserResult = addUserManager.AddUser (
                        new HumanName (
                            textFirstName.Text.Trim (),
                            textLastName.Text.Trim (),
                            textOtherName.Text.Trim ()
                        ),
                        settings: Settings,
                        email: textEmail.Text.Trim ().ToLower (),
                        useEmailAsUserName: PortalSettings.Registration.UseEmailAsUserName,
                        portalId: PortalId
                    );

                    if (addUserResult.UserCreateStatus == UserCreateStatus.Success) {
                        this.Message ("UserCreateStatus_Success.Text", MessageType.Success, true);

                        panelUserAdded.Visible = true;
                        panelUserInfo.Visible = false;
                        textLogin.Text = addUserResult.User.Username;
                        textPassword.Text = addUserResult.Password;

                        if (!string.IsNullOrEmpty (Settings.DoneUrl)) {
                            linkDone.NavigateUrl = FormatDoneUrl (Settings.DoneUrl, Settings.DoneUrlOpenInPopup, addUserResult.User.UserID);
                        }
                        else {
                            linkDone.NavigateUrl = Globals.NavigateURL ();
                        }
                    }
                    else if (addUserResult.UserCreateStatus == UserCreateStatus.UsernameAlreadyExists) {
                        this.Message (string.Format (
                            LocalizeString ("UserCreateStatus_UsernameAlreadyExists.Text"), addUserResult.User.Username),
                                      MessageType.Warning, false
                         );
                    }
                    else {
                        this.Message (string.Format (
                            LocalizeString ("UserCreateStatus_Other.Text"), addUserResult.UserCreateStatus.ToString ()),
                                      MessageType.Error, false
                        );
                    }
                }

            } catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        #endregion

        string FormatDoneUrl (string doneUrlTemplate, bool openInPopup, int userId)
        {
            var doneUrl = doneUrlTemplate.Replace ("[USERID]", userId.ToString ())
                                         .Replace ("[PORTALID]", PortalId.ToString ())
                                         .Replace ("[TABID]", TabId.ToString ());

            if (openInPopup) {
                return UrlUtils.PopUpUrl (doneUrl, PortalSettings, false, false, 550, 950);
            }

            return doneUrl;
        }
    }
}

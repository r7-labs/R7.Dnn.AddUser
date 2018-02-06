//
// AddUser.ascx.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2018 Volgograd State Agricultural University
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
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Membership;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.AddUser.Components;
using R7.Dnn.Extensions.ModuleExtensions;

namespace R7.Dnn.AddUser
{
    public class AddUser : PortalModuleBase
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
                    var addUserManager = new AddUserManager ();
                    var addUserResult = addUserManager.AddUser (
                        firstName: textFirstName.Text.Trim (),
                        lastName: textLastName.Text.Trim (),
                        otherName: textOtherName.Text.Trim (),
                        // TODO: Get displayNameFormat from settings
                        displayNameFormat: "LastName FirstName OtherName",
                        email: textEmail.Text.Trim ().ToLower (),
                        portalId: PortalId
                    );

                    if (addUserResult.UserCreateStatus == UserCreateStatus.Success) {
                        // TODO: Localize message
                        this.Message (addUserResult.UserCreateStatus.ToString (), MessageType.Success, false);
                        panelUserAdded.Visible = true;
                        panelUserInfo.Visible = false;
                        textLogin.Text = addUserResult.UserName;
                        textPassword.Text = addUserResult.Password;

                        linkDone.NavigateUrl = Globals.NavigateURL ();
                    }
                    else {
                        // TODO: Localize message
                        this.Message (addUserResult.UserCreateStatus.ToString (), MessageType.Error, false);
                    }
                }

            } catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        #endregion
    }
}

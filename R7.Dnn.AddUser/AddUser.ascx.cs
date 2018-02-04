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
using DotNetNuke.Services.Exceptions;

namespace R7.Dnn.AddUser
{
    public class AddUser : PortalModuleBase
    {
        #region Controls

        protected LinkButton buttonAddUser;
        protected HyperLink linkCancel;

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
                ModuleController.SynchronizeModule (ModuleId);
                Response.Redirect (Globals.NavigateURL (), true);

            } catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        #endregion
    }
}

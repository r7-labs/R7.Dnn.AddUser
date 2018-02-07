//
// AddUserManager.cs
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
using System.Collections.Generic;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Membership;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.AddUser.Models;

namespace R7.Dnn.AddUser.Components
{
    class AddUserManager
    {
        public AddUserResult AddUser (string firstName, string lastName, string otherName,
                                      AddUserSettings settings,
                                      string email, int portalId)
        {
            var user = new UserInfo {
                FirstName = firstName,
                LastName = lastName,
                DisplayName = FormatDisplayName (settings.DisplayNameFormat, firstName, lastName, otherName),
                Email = email,
                // TODO: Generate Username?
                Username = email,
                PortalID = portalId
            };

            var password = UserController.GeneratePassword ();
            user.Membership.Password = password;

            UserCreateStatus userCreateStatus = UserController.CreateUser (ref user);

            if (userCreateStatus == UserCreateStatus.Success) {
                try {
                    AssignUserToRoles (user, settings.RoleIds, portalId);
                }
                catch (Exception ex) {
                    userCreateStatus = UserCreateStatus.UnexpectedError;
                    Exceptions.LogException (new SecurityException ("Cannot assign user to roles", ex));
                }
            }
            
            return new AddUserResult {
                UserCreateStatus = userCreateStatus,
                User = user,
                Password = password
            };
        }

        void AssignUserToRoles (UserInfo user, IEnumerable<int> roleIds, int portalId)
        {
            foreach (var roleId in roleIds) {
                var role = RoleController.Instance.GetRoleById (portalId, roleId);
                if (role != null) {
                    RoleController.Instance.AddUserRole (portalId, user.UserID, role.RoleID,
                                                             RoleStatus.Approved, false,
                                                             DateTime.MinValue, DateTime.MinValue);
                }
            }
        }


        // TODO: Add tests
        string FormatDisplayName (string displayNameFormat, string firstName, string lastName, string otherName)
        {
            return displayNameFormat.Replace ("FirstName", firstName)
                                    .Replace ("F.", AppendIfNotEmpty (FirstCharOrEmpty (firstName), "."))
                                    .Replace ("LastName", lastName)
                                    .Replace ("L.", AppendIfNotEmpty (FirstCharOrEmpty (lastName), "."))
                                    .Replace ("OtherName", otherName)
                                    .Replace ("O.", AppendIfNotEmpty (FirstCharOrEmpty (otherName), "."))
                                    .Replace ("  ", " ");
        }

        string FirstCharOrEmpty (string value) =>
            (value.Length > 0)? value [0].ToString () : string.Empty;

        string AppendIfNotEmpty (string value1, string value2) =>
            string.IsNullOrEmpty (value1)? value1 : value1 + value2;
    }
}

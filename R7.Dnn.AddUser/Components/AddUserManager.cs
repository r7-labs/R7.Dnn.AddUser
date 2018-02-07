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
using R7.Dnn.AddUser.Models;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Membership;
using DotNetNuke.Security.Roles;

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
                foreach (var roleId in settings.RoleIds) {
                    var role = RoleController.Instance.GetRoleById (portalId, roleId);
                    if (role != null) {
                        RoleController.Instance.AddUserRole (portalId, user.UserID, role.RoleID,
                                                             RoleStatus.Approved, false,
                                                             DateTime.MinValue, DateTime.MinValue);
                    }
                }
            }
            
            return new AddUserResult {
                UserCreateStatus = userCreateStatus,
                UserId = user.UserID,
                UserName = user.Username,
                Password = password
            };
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

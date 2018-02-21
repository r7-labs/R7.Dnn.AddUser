//
// AddUserManager.cs
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
using System.Text.RegularExpressions;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Membership;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.AddUser.Models;
using Unidecode.NET;

namespace R7.Dnn.AddUser.Components
{
    class AddUserManager
    {
        public AddUserResult AddUser (string firstName, string lastName, string otherName,
                                      AddUserSettings settings,
                                      string email, bool useEmailAsUserName, int portalId)
        {
            var userName = FormatUserName (settings.UserNameFormat, firstName, lastName, otherName, email, useEmailAsUserName);
            var user = new UserInfo {
                FirstName = firstName,
                LastName = lastName,
                DisplayName = FormatDisplayName (GetDisplayNameFormat (settings, PortalSettings.Current), firstName, lastName, otherName, userName),
                Email = email,
                Username = userName,
                PortalID = portalId
            };

            var password = GeneratePassword ();
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

        string GeneratePassword ()
        {
            return UserController.GeneratePassword (MembershipProviderConfig.MinPasswordLength);
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
        string FormatUserName (string userNameFormat,
                               string firstName, string lastName, string otherName,
                               string email, bool useEmailAsUserName)
        {
            if (useEmailAsUserName) {
                return email;
            }

            var userName = userNameFormat.Replace ("[FIRSTNAME]", firstName)
                                         .Replace ("[F]", FirstCharOrEmpty (firstName))
                                         .Replace ("[LASTNAME]", lastName)
                                         .Replace ("[L]", FirstCharOrEmpty (lastName))
                                         .Replace ("[OTHERNAME]", otherName)
                                         .Replace ("[O]", FirstCharOrEmpty (otherName))
                                         .Replace ("[EMAIL]", email.Replace ("@", " at "))
                                         .ToLower ()
                                         .Unidecode ();

            var userName2 = Regex.Replace (Regex.Replace (userName, @"[^a-z0-9]", "_"), @"_+", "_").Trim ('_');
            return (userName2.Length > 100)? userName2.Substring (0, 100) : userName2;
        }

        string GetDisplayNameFormat (AddUserSettings settings, PortalSettings portalSettings)
        {
            return !string.IsNullOrEmpty (settings.DisplayNameFormat)
                          ? settings.DisplayNameFormat
                              : portalSettings.Registration.DisplayNameFormat;
        }

        // TODO: Add tests
        string FormatDisplayName (string displayNameFormat, string firstName, string lastName, string otherName, string userName)
        {
            var displayName = displayNameFormat.Replace ("[USERNAME]", userName)
                                               .Replace ("[FIRSTNAME]", firstName)
                                               .Replace ("[F]", AppendIfNotEmpty (FirstCharOrEmpty (firstName), "."))
                                               .Replace ("[LASTNAME]", lastName)
                                               .Replace ("[L]", AppendIfNotEmpty (FirstCharOrEmpty (lastName), "."))
                                               .Replace ("[OTHERNAME]", otherName)
                                               .Replace ("[O]", AppendIfNotEmpty (FirstCharOrEmpty (otherName), "."));

            var displayName2 = Regex.Replace (displayName, @"\s+", " ");
            return (displayName2.Length > 128)? displayName2.Substring (0, 128) : displayName2;
        }

        string FirstCharOrEmpty (string value) =>
            (value.Length > 0)? value [0].ToString () : string.Empty;

        string AppendIfNotEmpty (string value1, string value2) =>
            string.IsNullOrEmpty (value1)? value1 : value1 + value2;
    }
}

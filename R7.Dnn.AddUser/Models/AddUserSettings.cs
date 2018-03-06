using System;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Entities.Modules.Settings;

namespace R7.Dnn.AddUser.Models
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class AddUserSettings
    {
        [ModuleSetting (Prefix = "AddUser_")]
        public string DisplayNameFormat { get; set; } = "[FIRSTNAME] [O] [LASTNAME]";

        [ModuleSetting (Prefix = "AddUser_")]
        public string UserNameFormat { get; set; } = "[LASTNAME]_[F][O]";

        [ModuleSetting (Prefix = "AddUser_")]
        public int? DesiredPasswordLength { get; set; }

        [ModuleSetting (Prefix = "AddUser_")]
        public string AllowedSpecialChars { get; set; }

        [ModuleSetting (Prefix = "AddUser_")]
        public string DoneUrl { get; set; }

        [ModuleSetting (Prefix = "AddUser_")]
        public bool DoneUrlOpenInPopup { get; set; }

        [ModuleSetting (Prefix = "AddUser_")]
        public string Roles { get; set; }

        public IEnumerable<int> RoleIds =>
            (Roles ?? string.Empty)
                .Split (";".ToCharArray (), StringSplitOptions.RemoveEmptyEntries)
                .Select (strRoleId => int.Parse (strRoleId));
    }
}

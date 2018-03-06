using System.Text.RegularExpressions;
using R7.Dnn.AddUser.Models;
using Unidecode.NET;

namespace R7.Dnn.AddUser.Components
{
    public class NameFormatter: INameFormatter
    {
        public string FormatUserName (string userNameFormat, HumanName name,
                               string email, bool useEmailAsUserName)
        {
            if (useEmailAsUserName) {
                return email;
            }

            var userName = userNameFormat.Replace ("[FIRSTNAME]", name.FirstName)
                                         .Replace ("[F]", FirstCharOrEmpty (name.FirstName))
                                         .Replace ("[LASTNAME]", name.LastName)
                                         .Replace ("[L]", FirstCharOrEmpty (name.LastName))
                                         .Replace ("[OTHERNAME]", name.OtherName)
                                         .Replace ("[O]", FirstCharOrEmpty (name.OtherName))
                                         .Replace ("[EMAIL]", email.Replace ("@", " at "))
                                         .ToLower ()
                                         .Unidecode ();

            var userName2 = Regex.Replace (Regex.Replace (userName, @"[^a-z0-9']", "_"), @"_+", "_").Trim ('_');
            return (userName2.Length > 100)? userName2.Substring (0, 100) : userName2;
        }

        public string FormatDisplayName (string displayNameFormat, HumanName name, string userName)
        {
            var displayName = displayNameFormat.Replace ("[USERNAME]", userName)
                                               .Replace ("[FIRSTNAME]", name.FirstName)
                                               .Replace ("[F]", AppendIfNotEmpty (FirstCharOrEmpty (name.FirstName), "."))
                                               .Replace ("[LASTNAME]", name.LastName)
                                               .Replace ("[L]", AppendIfNotEmpty (FirstCharOrEmpty (name.LastName), "."))
                                               .Replace ("[OTHERNAME]", name.OtherName)
                                               .Replace ("[O]", AppendIfNotEmpty (FirstCharOrEmpty (name.OtherName), "."));

            var displayName2 = Regex.Replace (displayName, @"\s+", " ");
            return (displayName2.Length > 128)? displayName2.Substring (0, 128) : displayName2;
        }

        string FirstCharOrEmpty (string value) =>
            (value.Length > 0)? value [0].ToString () : string.Empty;

        string AppendIfNotEmpty (string value1, string value2) =>
            string.IsNullOrEmpty (value1)? value1 : value1 + value2;
    }
}

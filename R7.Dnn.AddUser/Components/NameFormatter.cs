//
// NameFormatter.cs
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

using System.Text.RegularExpressions;
using R7.Dnn.AddUser.Models;
using Unidecode.NET;

namespace R7.Dnn.AddUser.Components
{
    // TODO: Add tests
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

            var userName2 = Regex.Replace (Regex.Replace (userName, @"[^a-z0-9]", "_"), @"_+", "_").Trim ('_');
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

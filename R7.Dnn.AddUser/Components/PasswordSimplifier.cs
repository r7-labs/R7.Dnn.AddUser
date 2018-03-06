using System;
using System.Collections.Generic;

namespace R7.Dnn.AddUser.Components
{
    public class PasswordSimplifier
    {
        public string ReduceVarietyOfSpecialChars (string password, string allowedSpecialChars)
        {
            var rnd = new Random ();
            rnd.Next ();

            var simplePassword = string.Empty;
            foreach (var pch in password) {
                if (char.IsLetterOrDigit (pch) || allowedSpecialChars.IndexOf (pch) >= 0) {
                    simplePassword += pch;
                }
                else {
                    simplePassword += allowedSpecialChars [rnd.Next (allowedSpecialChars.Length)];
                }
            }

            return simplePassword;
        }

        const string alphanumerics = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        const string specials = "_!?@#$%^&+-=*()[]{}<>.,:;~\\|/";

        public string MinifyNumberOfSpecialChars (string password, int minSpecialChars)
        {
            if (password.Length < minSpecialChars) {
                throw new ArgumentException (
                    "Password length must be greater or equal than min. special characters count."
                );
            }

            var rnd = new Random ();
            rnd.Next ();

            var specialCharPositions = new List<int> ();
            for (var i = 0; i < password.Length; i++) {
                if (!char.IsLetterOrDigit (password [i])) {
                    specialCharPositions.Add (i);
                }
            }

            var simplePassword = password;

            if (specialCharPositions.Count > minSpecialChars) {
                while (specialCharPositions.Count > minSpecialChars && specialCharPositions.Count > 0) {
                    var positionIndex = rnd.Next (specialCharPositions.Count);
                    simplePassword = ReplaceChar (simplePassword, specialCharPositions [positionIndex], alphanumerics [rnd.Next (alphanumerics.Length)]);
                    specialCharPositions.RemoveAt (positionIndex);
                }
            }
            else if (specialCharPositions.Count < minSpecialChars) {
                var numCharsToAdd = Math.Min (minSpecialChars - specialCharPositions.Count, simplePassword.Length - specialCharPositions.Count);
                while (numCharsToAdd > 0) {
                    var position = rnd.Next (simplePassword.Length);
                    if (char.IsLetterOrDigit (simplePassword [position])) {
                        simplePassword = ReplaceChar (simplePassword, position, specials [rnd.Next (specials.Length)]);
                        numCharsToAdd--;
                    }
                }
            }

            return simplePassword;
        }

        string ReplaceChar (string source, int position, char c)
        {
            var charArray = source.ToCharArray ();
            charArray [position] = c;
            return new string (charArray);
        }
    }
}

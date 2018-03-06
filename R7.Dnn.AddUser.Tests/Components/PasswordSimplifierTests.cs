using System;
using System.Linq;
using R7.Dnn.AddUser.Components;
using Xunit;

namespace R7.Dnn.AddUser.Tests.Components
{
    public class PasswordSimplifierTests
    {
        [Fact]
        public void ReduceVarietyOfSpecialCharsTest ()
        {
            var passwordSimplifier = new PasswordSimplifier ();
            Assert.Equal ("P___w0rd", passwordSimplifier.ReduceVarietyOfSpecialChars ("P@$$w0rd", "_"));
        }

        [Fact]
        public void MinifyNumberOfSpecialCharsTest ()
        {
            var passwordSimplifier = new PasswordSimplifier ();
            var password = "P@$$w0rd";

            Assert.Equal (0, passwordSimplifier.MinifyNumberOfSpecialChars (password, 0).Count (c => !char.IsLetterOrDigit (c)));
            Assert.Equal (1, passwordSimplifier.MinifyNumberOfSpecialChars (password, 1).Count (c => !char.IsLetterOrDigit (c)));
            Assert.Equal (3, passwordSimplifier.MinifyNumberOfSpecialChars (password, 3).Count (c => !char.IsLetterOrDigit (c)));
            Assert.Equal (5, passwordSimplifier.MinifyNumberOfSpecialChars (password, 5).Count (c => !char.IsLetterOrDigit (c)));             
            Assert.Equal (password.Length, passwordSimplifier.MinifyNumberOfSpecialChars (password, password.Length).Count (c => !char.IsLetterOrDigit (c)));

            Assert.Throws<ArgumentException> (() => passwordSimplifier.MinifyNumberOfSpecialChars (password, password.Length + 1));
        }
    }
}

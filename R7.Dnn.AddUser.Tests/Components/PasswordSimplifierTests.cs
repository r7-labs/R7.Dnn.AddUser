//
// PasswordSimplifierTests.cs
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

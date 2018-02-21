//
// NameFormatterTests.cs
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

using R7.Dnn.AddUser.Components;
using R7.Dnn.AddUser.Models;
using Xunit;

namespace R7.Dnn.AddUser.Tests.Components
{
    public class NameFormatterTests
    {
        [Fact]
        public void FormatDisplayNameTest ()
        {
            var nameFormatter = new NameFormatter ();
            var homer = new HumanName ("Homer", "Simpson", "Jay");
            var userName = string.Empty;

            Assert.Equal (
                "Homer J. Simpson",
                nameFormatter.FormatDisplayName ("[FIRSTNAME] [O] [LASTNAME]", homer, userName)
            );

            Assert.Equal (
                "Homer Jay Simpson",
                nameFormatter.FormatDisplayName ("[FIRSTNAME] [OTHERNAME] [LASTNAME]", homer, userName)
            );

            Assert.Equal (
                "Simpson, H.",
                nameFormatter.FormatDisplayName ("[LASTNAME], [F]", homer, userName)
            );
        }

        [Fact]
        public void FormatUserNameTest ()
        {
            var nameFormatter = new NameFormatter ();
            var homer = new HumanName ("Homer", "Simpson", "Jay");
            var homerRu = new HumanName ("Гомер", "Симпсон", "Джей");
            var email = "homer@simpsons.net";

            Assert.Equal (
                "homer_js",
                nameFormatter.FormatUserName ("[FIRSTNAME]_[O][L]", homer, email, false)
            );

            Assert.Equal (
                "simpson_gd",
                nameFormatter.FormatUserName ("[LASTNAME]_[F][O]", homerRu, email, false)
            );

            Assert.Equal (
                email,
                nameFormatter.FormatUserName ("[LASTNAME]_[F][O]", homer, email, true)
            );

            Assert.Equal (
                "homer_at_simpsons_net",
                nameFormatter.FormatUserName ("[EMAIL]", homer, email, false)
            );
        }
    }
}

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

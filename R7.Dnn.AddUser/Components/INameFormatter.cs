using R7.Dnn.AddUser.Models;

namespace R7.Dnn.AddUser.Components
{
    public interface INameFormatter
    {
        string FormatUserName (string userNameFormat, HumanName name, string email, bool useEmailAsUserName);

        string FormatDisplayName (string displayNameFormat, HumanName name, string userName);
    }
}

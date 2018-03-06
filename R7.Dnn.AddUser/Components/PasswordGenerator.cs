using System;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Membership;

namespace R7.Dnn.AddUser.Components
{
    public class PasswordGenerator: IPasswordGenerator
    {
        public string GeneratePassword (int? desiredLength)
        {
            if (desiredLength != null) {
                return UserController.GeneratePassword (Math.Max (desiredLength.Value, MinLength));
            }

            return UserController.GeneratePassword ();
        }

        public int MinLength => MembershipProviderConfig.MinPasswordLength;
    }
}

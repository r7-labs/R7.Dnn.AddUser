using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Membership;

namespace R7.Dnn.AddUser.Models
{
    class AddUserResult
    {
        public UserCreateStatus UserCreateStatus { get; set; }

        public UserInfo User { get;  set; }

        public string Password { get; set; }
    }
}

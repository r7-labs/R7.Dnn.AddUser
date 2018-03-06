namespace R7.Dnn.AddUser.Models
{
    public struct HumanName
    {
        public string FirstName;

        public string LastName;

        public string OtherName;

        public HumanName (string firstName, string lastName, string otherName)
        {
            FirstName = firstName;
            LastName = lastName;
            OtherName = otherName;
        }
    }
}

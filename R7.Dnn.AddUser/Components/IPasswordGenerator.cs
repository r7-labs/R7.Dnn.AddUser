namespace R7.Dnn.AddUser.Components
{
    public interface IPasswordGenerator
    {
        string GeneratePassword (int? desiredLength);

        int MinLength { get; }
    }
}

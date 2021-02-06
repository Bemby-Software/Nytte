namespace Nytte.Email
{
    public interface IEmailService
    {
        bool IsValidEmailAddress(string email);
    }
}
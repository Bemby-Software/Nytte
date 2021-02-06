using EmailValidation;

namespace Nytte.Email
{
    public class EmailService : IEmailService
    {
        public bool IsValidEmailAddress(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && EmailValidator.Validate(email);
        }
    }
}
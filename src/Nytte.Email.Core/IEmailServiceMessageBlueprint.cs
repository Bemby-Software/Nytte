namespace Nytte.Email.Core
{
    public interface IEmailServiceMessageBlueprint
    {
        string RecipientName { get; }
        string RecipientEmailAddress { get; }
        string EmailSubject { get; }
    }
}
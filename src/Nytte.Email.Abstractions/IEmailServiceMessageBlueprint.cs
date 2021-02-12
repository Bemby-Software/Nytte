namespace Nytte.Email.Abstractions
{
    public interface IEmailServiceMessageBlueprint
    {
        string RecipientName { get; }
        string RecipientEmailAddress { get; }
        string EmailSubject { get; }
    }
}
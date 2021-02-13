namespace Nytte.Email.Abstractions
{
    public interface IEmailBuilderFactory
    {
        IEmailServiceMessageBuilder ResolveBuilder<TBlueprint>() where TBlueprint : IEmailServiceMessageBlueprint;
    }
}
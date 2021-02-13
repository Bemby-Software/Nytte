using System.Threading.Tasks;
using MimeKit;
using Nytte.Email.Abstractions;
using Nytte.Email.Exceptions;
using Nytte.Email.Razor.Abstractions;

namespace Nytte.Email.Razor
{
    public class RazorEmailMessageBuilder : IRazorEmailMessageBuilder
    {
        private readonly IRazorPageStringRenderer _razorPageStringRenderer;

        public RazorEmailMessageBuilder(IRazorPageStringRenderer razorPageStringRenderer)
        {
            _razorPageStringRenderer = razorPageStringRenderer;
        }
        
        public MimeMessage BuildMessage<T>(T messageBlueprint) where T : IEmailServiceMessageBlueprint
        {
            var blueprint = messageBlueprint as RazorEmailMessageBlueprint;
            if (blueprint is null)
                throw new UnsupportedEmailServiceMessageBlueprintTypeException(typeof(T),
                    new[] {typeof(RazorEmailMessageBlueprint)});

            MimeMessage email = new MimeMessage();

            email.To.Add(new MailboxAddress(blueprint.RecipientName, blueprint.RecipientEmailAddress));
            email.From.Add(new MailboxAddress("Rob", "robertbennett1998@outlook.com"));
            email.Subject = blueprint.EmailSubject;

            
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = blueprint.RazorViewModelType is not null ? _razorPageStringRenderer.Render(blueprint.RazorViewName, blueprint.RazorViewModel) 
                                                                            : _razorPageStringRenderer.Render(blueprint.RazorViewName);
            
            email.Body = bodyBuilder.ToMessageBody();
            
            return email;
        }

        public async Task<MimeMessage> BuildMessageAsync<T>(T messageBlueprint) where T : IEmailServiceMessageBlueprint
        {
            var blueprint = messageBlueprint as RazorEmailMessageBlueprint;
            if (blueprint is null)
                throw new UnsupportedEmailServiceMessageBlueprintTypeException(typeof(T),
                    new[] {typeof(RazorEmailMessageBlueprint)});

            MimeMessage email = new MimeMessage();

            email.To.Add(new MailboxAddress(blueprint.RecipientName, blueprint.RecipientEmailAddress));
            email.From.Add(new MailboxAddress("Rob", "robertbennett1998@outlook.com"));
            email.Subject = blueprint.EmailSubject;

            
            var bodyBuilder = new BodyBuilder();
            if (blueprint.RazorViewModelType is not null)
            {
                bodyBuilder.HtmlBody = await _razorPageStringRenderer.RenderAsync(blueprint.RazorViewName, blueprint.RazorViewModel);
            }
            else
            {
                bodyBuilder.HtmlBody = await _razorPageStringRenderer.RenderAsync(blueprint.RazorViewName);
            }
            
            email.Body = bodyBuilder.ToMessageBody();
            
            return email;
        }
    }
}
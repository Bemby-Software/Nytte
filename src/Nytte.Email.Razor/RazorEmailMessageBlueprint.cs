using System;
using Nytte.Email.Core;

namespace Nytte.Email.Razor
{
    public class RazorEmailMessageBlueprint : IEmailServiceMessageBlueprint
    {
        public RazorEmailMessageBlueprint(string recipientName, string recipientEmailAddress, string emailSubject, string razorViewName, object razorViewModel=null, Type razorViewModelType=null)
        {
            RecipientName = recipientName;
            RecipientEmailAddress = recipientEmailAddress;
            EmailSubject = emailSubject;
            RazorViewName = razorViewName;
            RazorViewModel = razorViewModel;
            RazorViewModelType = razorViewModelType;
        }

        public string RecipientName { get; }
        public string RecipientEmailAddress { get; }
        public string EmailSubject { get; }
        public string RazorViewName { get; set; }
        public object RazorViewModel { get; set; }
        public Type RazorViewModelType { get; set; }
    }
    
    public class RazorEmailMessageBlueprint<T> : RazorEmailMessageBlueprint
    {
        public RazorEmailMessageBlueprint(string recipientName, string recipientEmailAddress, string emailSubject, string razorViewName, T razorViewModel) : base(recipientName, recipientEmailAddress, emailSubject, razorViewName, razorViewModel, typeof(T))
        {
        }
    }
}
using System;
using Nytte.Email.Abstractions;
using Nytte.Email.Razor.Abstractions;

namespace Nytte.Email.Razor
{
    public class RazorEmailMessageBlueprint : IRazorEmailMessageBlueprint
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
        public string RazorViewName { get; }
        public object RazorViewModel { get; }
        public Type RazorViewModelType { get; }
    }
    
    public class RazorEmailMessageBlueprint<T> : RazorEmailMessageBlueprint
    {
        public RazorEmailMessageBlueprint(string recipientName, string recipientEmailAddress, string emailSubject, string razorViewName, T razorViewModel) : base(recipientName, recipientEmailAddress, emailSubject, razorViewName, razorViewModel, typeof(T))
        {
        }
    }
}
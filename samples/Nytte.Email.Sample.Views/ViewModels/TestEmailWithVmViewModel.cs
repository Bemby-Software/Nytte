namespace Nytte.Email.Sample.Views.ViewModels
{
    public class TestEmailWithVmViewModel
    {
        public TestEmailWithVmViewModel()
        {
            
        }
        
        public TestEmailWithVmViewModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
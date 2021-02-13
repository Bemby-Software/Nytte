using System.Threading.Tasks;

namespace Nytte.Email.Razor.Abstractions
{
    public interface IRazorPageStringRenderer
    {
        string Render(string viewName);
        string Render<T>(string viewName, T viewModel);
        Task<string> RenderAsync(string viewName);
        Task<string> RenderAsync<T>(string viewName, T viewModel);
    }
}
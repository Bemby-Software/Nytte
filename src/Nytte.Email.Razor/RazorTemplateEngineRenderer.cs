using System.Threading.Tasks;
using Nytte.Email.Razor.Abstractions;
using Razor.Templating.Core;

namespace Nytte.Email.Razor
{
    public class RazorTemplateEngineRenderer : IRazorPageStringRenderer
    {
        public string Render(string viewName)
        {
            var renderTask = RazorTemplateEngine.RenderAsync(viewName);
            renderTask.Wait();
            
            return renderTask.Result;
        }

        public string Render<T>(string viewName, T viewModel)
        {
            var renderTask = RazorTemplateEngine.RenderAsync(viewName, viewModel);
            renderTask.Wait();
            
            return renderTask.Result;
        }

        public async Task<string> RenderAsync(string viewName)
        {
            return await RazorTemplateEngine.RenderAsync(viewName);
        }

        public async Task<string> RenderAsync<T>(string viewName, T viewModel)
        {
            return await RazorTemplateEngine.RenderAsync(viewName, viewModel);
        }
    }
}
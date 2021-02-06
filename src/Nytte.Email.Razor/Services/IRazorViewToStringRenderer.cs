using System.Threading.Tasks;

namespace Nytte.Email.Razor.Services
{
    public interface IRazorViewToStringRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
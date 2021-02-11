using System;
using System.Threading.Tasks;

namespace Nytte.Modules.Requests.Abstractions
{
    public delegate Task<string> ScopedRequestHandlerDelegateAsync(IServiceProvider serviceProvider,
        string packedRequest);
}
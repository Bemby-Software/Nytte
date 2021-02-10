using System;
using System.Threading.Tasks;

namespace Nytte.Modules.Requests
{
    public delegate Task<string> ScopedRequestHandlerDelegateAsync(IServiceProvider serviceProvider,
        string packedRequest);
}
using System;

namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleRequestSpecification
    {
        ScopedRequestHandlerDelegateAsync AsyncHandler { get; }
        
        Type ReturnType { get; }
        
        Type QueryType { get; }
        
        string Key { get; }
    }
}
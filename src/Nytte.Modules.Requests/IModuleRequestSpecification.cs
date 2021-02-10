using System;

namespace Nytte.Modules.Requests
{
    public interface IModuleRequestSpecification
    {
        ScopedRequestHandlerDelegateAsync AsyncHandler { get; }
        
        Type ReturnType { get; }
        
        Type QueryType { get; }
        
        string Key { get; }
    }
}
using System;
using Nytte.Modules.Requests.Abstractions;

namespace Nytte.Modules.Requests
{
    public class ModuleRequestSpecification : IModuleRequestSpecification
    {
        public ScopedRequestHandlerDelegateAsync AsyncHandler { get; }
        public Type ReturnType { get; }
        public Type QueryType { get; }
        public string Key { get; }

        public ModuleRequestSpecification(ScopedRequestHandlerDelegateAsync asyncHandler, Type returnType, Type queryType, string key)
        {
            AsyncHandler = asyncHandler;
            ReturnType = returnType;
            QueryType = queryType;
            Key = key;
        }
    }
}
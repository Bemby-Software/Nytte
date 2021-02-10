using System;

namespace Nytte.Modules.Requests.Abstractions
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
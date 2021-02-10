using System;
using System.Collections.Generic;
using System.Linq;
using Nytte.Modules.Requests.Abstractions;

namespace Nytte.Modules.Requests
{
    public class ModuleRequestRegistry : IModuleRequestRegistry
    {
        private readonly IModuleRequestFactory _factory;
        private readonly List<IModuleRequestSpecification> _requests;
        private readonly object _requestsLock = new();

        public ModuleRequestRegistry(IModuleRequestFactory factory)
        {
            _factory = factory;
            _requests = new List<IModuleRequestSpecification>();
        }
        
        public IModuleRequestSpecification GetRequest<TReturns, TQuery>() where TReturns : class where TQuery : IModuleQuery
        {
            var key = _factory.GetKey<TReturns, TQuery>();

            IModuleRequestSpecification spec;
            
            lock (_requestsLock)
            {
                spec = _requests.FirstOrDefault(x => x.Key == key);
            }

            if (spec is null)
                throw new InvalidOperationException($"No handler defined for type with key {key}");

            return spec;
        }

        public void Add(IModuleRequestSpecification specification)
        {
            lock (_requestsLock)
            {
                if (_requests.Any(x => x.Key == specification.Key))
                    throw new InvalidOperationException(
                        $"Handler already defined for request with key {specification.Key}");
                
                _requests.Add(specification);
            }
        }
    }
}
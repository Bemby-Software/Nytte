using System;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Modules.Requests.Abstractions;
using Nytte.Wrappers;

namespace Nytte.Modules.Requests
{
    public class ModuleRequestFactory : IModuleRequestFactory
    {
        public ScopedRequestHandlerDelegateAsync CreateHandler<TReturns, TQuery>() where TReturns : class where TQuery : IModuleQuery
        {
            return async (provider, packedRequest) =>
            {
                var json = provider.GetRequiredService<IJson>();

                var query = await json.DeserializeAsync<TQuery>(packedRequest);

                using var scope = provider.CreateScope();

                var handler = scope.ServiceProvider.GetRequiredService<IModuleQueryHandler<TReturns, TQuery>>();

                if (handler is null)
                {
                    throw new InvalidOperationException(
                        $"There is no request handler defined for query {typeof(TQuery).FullName} with return type {typeof(TReturns).FullName}.");
                }

                var result = await handler.HandleAsync(query);
                return await json.SerializeAsync(result);
            };
        }

        public string GetKey<TReturns, TQuery>() where TReturns : class where TQuery : IModuleQuery
        {
            var queryType = typeof(TQuery);
            var returnType = typeof(TReturns);

            var owner = queryType.GetAttribute<ModuleOwnerAttribute>();

            if (owner is null)
                throw new InvalidOperationException("A owner must be specified for this query using the [ModuleOwner] attribute");

            return $"{owner.ModuleName}/{queryType.Name}->{returnType.Name}";
        }

        public IModuleRequestSpecification Create<TReturns, TQuery>(string key, ScopedRequestHandlerDelegateAsync handler) 
            where TReturns : class 
            where TQuery : IModuleQuery 
            => new ModuleRequestSpecification(handler, typeof(TReturns), typeof(TQuery), key);
    }
}
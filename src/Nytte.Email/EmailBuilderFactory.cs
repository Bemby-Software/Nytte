using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Email.Abstractions;

namespace Nytte.Email
{
    public class EmailBuilderFactory : IEmailBuilderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmailBuilderRegister _emailBuilderRegister;

        public EmailBuilderFactory(IServiceProvider serviceProvider, IEmailBuilderRegister emailBuilderRegister)
        {
            _serviceProvider = serviceProvider;
            _emailBuilderRegister = emailBuilderRegister;
        }
        
        public IEmailServiceMessageBuilder ResolveBuilder<TBlueprint>() where TBlueprint : IEmailServiceMessageBlueprint
        {
            var builderTypes = _emailBuilderRegister.GetBuildersForBlueprint<TBlueprint>();

            if (builderTypes.Count > 1)
                throw new NotSupportedException(
                    $"Multiple builders registered for the blueprint type {typeof(TBlueprint).FullName}.");
            
            return _serviceProvider.GetRequiredService(builderTypes.First()) as IEmailServiceMessageBuilder;
        }
    }
}
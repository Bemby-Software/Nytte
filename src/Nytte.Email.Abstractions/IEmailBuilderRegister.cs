using System;
using System.Collections.Generic;

namespace Nytte.Email.Abstractions
{
    public interface IEmailBuilderRegister
    {
        void RegisterBuilderForBlueprint<TBlueprint, TBuilder>() where TBlueprint : IEmailServiceMessageBlueprint where TBuilder : IEmailServiceMessageBuilder;
        IReadOnlyList<Type> GetBuildersForBlueprint<TBlueprint>() where TBlueprint : IEmailServiceMessageBlueprint;
    }
}
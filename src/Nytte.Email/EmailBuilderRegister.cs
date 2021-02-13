using System;
using System.Collections.Generic;
using System.Linq;
using Nytte.Email.Abstractions;

namespace Nytte.Email
{
    public class EmailBuilderRegister : IEmailBuilderRegister
    {
        private Dictionary<Type, IList<Type>> _builders =
            new Dictionary<Type, IList<Type>>();
        
        public void RegisterBuilderForBlueprint<TBlueprint, TBuilder>() where TBlueprint : IEmailServiceMessageBlueprint where TBuilder : IEmailServiceMessageBuilder
        {
            if (_builders.ContainsKey(typeof(TBlueprint)))
            {
                _builders[typeof(TBlueprint)].Add(typeof(TBuilder));
            }
            else
            {
                _builders[typeof(TBlueprint)] = new List<Type>(new [] {typeof(TBuilder)});
            }
        }

        public IReadOnlyList<Type> GetBuildersForBlueprint<TBlueprint>() where TBlueprint : IEmailServiceMessageBlueprint
        {
            var blueprintType = typeof(TBlueprint);

            // Make sure it can handle the case when the blueprint type is generic as it has a view model...
            if (blueprintType.IsGenericType)
                blueprintType = blueprintType.BaseType;

            return _builders[blueprintType].ToList();
        }
    }
}
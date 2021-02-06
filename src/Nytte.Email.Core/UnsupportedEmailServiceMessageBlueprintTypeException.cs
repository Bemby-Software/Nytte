using System;

namespace Nytte.Email.Core
{
    public class UnsupportedEmailServiceMessageBlueprintTypeException : Exception
    {
        public UnsupportedEmailServiceMessageBlueprintTypeException(Type unsupportedType, Type[] supportedTypes) : base(message: $"Blueprint type {unsupportedType.FullName} is not supported. Supported types are: {supportedTypes.FullNameToSemiColonSeparatedString()}")
        {
            
        }
    }
}
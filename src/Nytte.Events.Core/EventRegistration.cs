using System;
using System.Threading.Tasks;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core
{
    public class EventRegistration : IEventRegistration
    {
        public Type Type { get; }
        public string Key { get; }
        
        
        public ScopedEventHandlerAsync AsyncHandler { get; }

        private EventRegistration(Type type, string key, ScopedEventHandlerAsync asyncHandler)
        {
            Type = type;
            Key = key;
            AsyncHandler = asyncHandler;
        }

        public static IEventRegistration Create<TEvent>(string key, ScopedEventHandlerAsync asyncHandler) 
            => new EventRegistration(typeof(TEvent), key, asyncHandler);
    }
}
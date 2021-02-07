

namespace Nytte.Events.Abstractions
{
    public class EventsOptions
    {
        /// <summary>
        /// Whether or not to prefix event keys with the name specified in the [EventOwner] attribute.
        /// This requires all events to declare the attribute [EventOwner]
        /// </summary>
        public bool UseOwnersInKeys { get; set; } = true;

        /// <summary>
        /// Whether or not throw exceptions when no handlers are registered.
        /// </summary>
        public bool UseStrictMode { get; set; } = true;
    }
}
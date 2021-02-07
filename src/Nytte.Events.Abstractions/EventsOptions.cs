

namespace Nytte.Events.Abstractions
{
    public class MemoryEventsOptions
    {
        /// <summary>
        /// Whether or not to prefix event keys with the name specified in the [EventOwner] attribute.
        /// </summary>
        public bool UseOwnersInKeys { get; set; } = false;

        /// <summary>
        /// Whether or not throw exceptions when no handlers are registered. When using owners in keys exceptions will be thrown if not owner is declared on events.
        /// </summary>
        public bool UseStrictMode { get; set; } = true;
    }
}
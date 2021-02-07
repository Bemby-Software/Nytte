using System;

namespace Nytte.Events.Abstractions
{
    public class EventOwnerAttribute : Attribute
    {
        public string Owner { get; }

        public EventOwnerAttribute(string owner)
        {
            Owner = owner;
        }
    }
}
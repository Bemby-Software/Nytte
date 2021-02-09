using System.Text.Json.Serialization;
using Nytte.Events.Abstractions;

namespace Nytte.Sample.ModuleA.Events.External
{
    [EventOwner("ModuleB")]
    public class SampleMessage : IEvent
    {
        [JsonConstructor]
        public SampleMessage(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
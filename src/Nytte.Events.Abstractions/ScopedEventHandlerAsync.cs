using System;
using System.Threading.Tasks;

namespace Nytte.Events.Abstractions
{
    public delegate Task ScopedEventHandlerAsync(IServiceProvider serviceProvider, string packedEvent);
}
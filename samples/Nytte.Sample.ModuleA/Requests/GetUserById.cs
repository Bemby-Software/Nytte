using System;
using Nytte.Modules.Requests.Abstractions;

namespace Nytte.Sample.ModuleA.Requests
{
    [ModuleOwner("ModuleA")]
    public class GetUserById : IModuleRequest
    {
        public Guid Id { get; }

        public GetUserById(Guid id)
        {
            Id = id;
        }
    }
}
using System;
using Nytte.Modules.Requests;

namespace Nytte.Sample.ModuleB.Queries
{
    [ModuleOwner("ModuleB")]
    public class GetUserById : IModuleQuery
    {
        public Guid Id { get; set; }

        public GetUserById(Guid id)
        {
            Id = id;
        }
    }
}
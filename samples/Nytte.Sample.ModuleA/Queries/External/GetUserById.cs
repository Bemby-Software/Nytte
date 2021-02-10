using System;
using Nytte.Modules.Requests;

namespace Nytte.Sample.ModuleA.Queries.External
{
    public class GetUserById : IModuleQuery
    {
        public Guid Id { get; set; }

        public GetUserById(Guid id)
        {
            Id = id;
        }
    }
}
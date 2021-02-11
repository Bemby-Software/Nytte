using System;

namespace Nytte.Modules.Requests.Abstractions
{
    public class ModuleOwnerAttribute : Attribute
    {
        public ModuleOwnerAttribute(string moduleName)
        {
            ModuleName = moduleName;
        }

        public string ModuleName { get; }
    }
}
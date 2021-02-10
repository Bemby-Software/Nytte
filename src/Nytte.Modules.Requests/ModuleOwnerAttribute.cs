using System;

namespace Nytte.Modules.Requests
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
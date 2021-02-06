using System;

namespace Nytte.Email.Core
{
    public static class TypeArrayExtensions
    {
        public static string FullNameToSemiColonSeparatedString(this Type[] types)
        {
            string fullnames = "";
            foreach (var type in types)
            {
                fullnames += $"{type.FullName}; ";
            }

            return fullnames;
        }
    }
}
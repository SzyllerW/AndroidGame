using System;
using System.Runtime.CompilerServices;

namespace Commands
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]

    public class CommandAttribute : Attribute
    {
        public readonly string CommandName;
        public readonly string CommandDescription;

        public CommandAttribute (string commandName, string commandDescription)
        {
            CommandName = commandName;
            CommandDescription = commandDescription;
        }
    }
}

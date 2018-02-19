using System;

namespace FileTag.MVVM
{
    public interface INamedCommands
    {
        INamedCommands Add(string name, Action<object> action);

        void Execute(string name, object param = null);
    }
}

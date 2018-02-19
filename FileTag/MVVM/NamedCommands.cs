using System;
using System.Collections.Generic;

namespace FileTag.MVVM
{
    public class NamedCommands : INamedCommands
    {
        public NamedCommands()
        {
            _dictionary = new Dictionary<string, Action<object>>();
        }

        private Dictionary<string, Action<object>> _dictionary;

        public INamedCommands Add(string name, Action<object> action)
        {
            if (action == null || string.IsNullOrEmpty(name) || _dictionary.ContainsKey(name))
                throw new ArgumentException();

            _dictionary.Add(name, action);

            return this;
        }

        public void Execute(string name, object param = null)
        {
            if (string.IsNullOrEmpty(name) || !_dictionary.ContainsKey(name))
                    throw new ArgumentException($"Command {name} doesn't exist");

            _dictionary[name].Invoke(param);
        }
    }
}

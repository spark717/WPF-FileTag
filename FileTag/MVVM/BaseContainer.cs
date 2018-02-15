using System;

namespace FileTag.MVVM
{
    public abstract class BaseContainer<T> : IContainer<T>, IComparable where T: class
    {
        public BaseContainer(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is T)
                if (obj == Data)
                    return 0;

            return -1;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}

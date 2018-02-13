using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTag.Infrastacture.Watcher
{
    public enum EventType
    {
        Created,
        Deleted,
        Changed,
        Renamed
    }
}

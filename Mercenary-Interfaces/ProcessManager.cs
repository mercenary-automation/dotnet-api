using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Mercenary.Interfaces
{
    public static ConcurrentBag<Process> ProcessManager = new ConcurrentBag<Process>();
}

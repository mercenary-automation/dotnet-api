using System;
using Newtonsoft.Json.Linq;

namespace Mercenary.Interfaces
{
    public interface TaskPlugin
    {
        Boolean Initialize(JObject parameters);
        JObject Execute(JObject parameters);
    }
}

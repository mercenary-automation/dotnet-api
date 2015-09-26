using System;
using Newtonsoft.Json.Linq;

namespace Mercenary.Interfaces
{
    public interface ReportPlugin
    {
        Boolean Initialize(JObject parameters);
        Boolean Execute(JObject parameters);
    }
}

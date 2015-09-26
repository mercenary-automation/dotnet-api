using System;
using System.ComponentModel.Composition;
using Mercenary.Interfaces;
using Newtonsoft.Json.Linq;

namespace $rootnamespace$
{
    [Export(typeof(TaskPlugin))]
    [ExportMetadata("Type", "task-example")]
    public class ExampleTaskPlugin : TaskPlugin
    {
        public bool Initialize(JObject parameters)
        {
            throw new NotImplementedException();
        }

        public JObject Execute(JObject parameters)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.ComponentModel.Composition;
using Mercenary.Interfaces;
using Newtonsoft.Json.Linq;

namespace $rootnamespace$
{
    [Export(typeof(ReportPlugin))]
    [ExportMetadata("Type", "report-example")]
    public class ExampleReportPlugin : ReportPlugin
    {
        public bool Initialize(JObject parameters)
        {
            throw new NotImplementedException();
        }

        public bool Execute(JObject parameters)
        {
            throw new NotImplementedException();
        }
    }
}

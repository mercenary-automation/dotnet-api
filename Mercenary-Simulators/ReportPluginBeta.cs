using System;
using System.Diagnostics;
using System.ComponentModel.Composition;
using Mercenary.Interfaces;
using Newtonsoft.Json.Linq;

namespace Mercenary.Simulators
{
    [Export(typeof(ReportPlugin))]
    [ExportMetadata("Type", "beta")]
    class ReportPluginBeta : ReportPlugin
    {
        private JObject parameters;
        private JObject data;

        public ReportPluginBeta()
        {
            this.Label = "Report Plugin Beta";
        }

        public bool Initialize(JObject parameters)
        {
            this.parameters = parameters;

            // validate parameters, validate can initialize

            this.Initialized = true;
            return this.Initialized;
        }

        public bool Execute(JObject parameters)
        {
            if (this.Initialized)
            {
                // do your thing
                return true;
            }
            else
            {
                throw new ApplicationException(this.Label + " Not Initialized : Plugin must be initialzed prior to execution.");
            }
        }

        public string Label { get; private set; }

        public bool Initialized { get; private set; }
    }
}

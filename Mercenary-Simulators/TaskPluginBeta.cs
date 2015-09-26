using System;
using System.ComponentModel.Composition;
using Mercenary.Interfaces;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Mercenary.Simulators
{
    [Export(typeof(TaskPlugin))]
    [ExportMetadata("Type", "beta")]
    class TaskPluginBeta : TaskPlugin
    {
        private JObject parameters;
        private JObject data;
        private Process process;
        private string command;

        public TaskPluginBeta()
        {
            this.Label = "Task Plugin Beta";

            this.StartInfo = new ProcessStartInfo();
            this.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            this.StartInfo.FileName = "cmd.exe";
        }

        public bool Initialize(JObject parameters)
        {
            this.parameters = parameters;

            // validate parameters, validate can initialize

            this.Initialized = true;
            return this.Initialized;
        }

        public JObject Execute(JObject parameters)
        {
            if (this.Initialized)
            {
                this.data = parameters;
                // edit the command based on the parameters

                this.StartInfo.Arguments = "/C" + this.command;

                this.process = new Process();
                process.StartInfo = this.StartInfo;
                process.Start();
                process.WaitForExit();

                return this.data;
            }
            else
            {
                throw new ApplicationException(this.Label + " Not Initialized : Plugin must be initialzed prior to execution.");
            }
        }

        public ProcessStartInfo StartInfo { get; private set; }

        public string Label { get; private set; }

        public bool Initialized { get; private set; }
    }
}

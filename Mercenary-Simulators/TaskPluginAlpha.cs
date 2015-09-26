using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Mercenary.Interfaces;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Mercenary.Simulators
{
    [Export(typeof(TaskPlugin))]
    [ExportMetadata("Type", "alpha")]
    class TaskPluginAlpha : TaskPlugin
    {
        #region Private Variables

        private JObject parameters;
        private JObject data;

        private ProcessStartInfo startinfo;
        private Process process;

        private string command; 
        private List<string> output;
        
        private string label;
        private bool initialized;

        #endregion

        public TaskPluginAlpha()
        {
            this.label = "Task Plugin Alpha";

            this.startinfo = new ProcessStartInfo();
            this.startinfo.RedirectStandardOutput = true; 
            this.startinfo.WindowStyle = ProcessWindowStyle.Hidden;
            this.startinfo.FileName = "cmd.exe";
        }

        public virtual bool Initialize(JObject parameters)
        {
            this.parameters = parameters;
            this.initialized = true;

            // validate parameters, validate can initialize

            return this.initialized;
        }

        public virtual JObject Execute(JObject parameters)
        {
            this.output = new List<string>();

            if (this.initialized)
            {
                this.data = parameters;

                // edit the command based on the parameters
                this.command = "/C";
                
                // add the command to the start info args
                this.startinfo.Arguments = this.command;

                // start the process
                this.process = new Process();
                this.process.StartInfo = this.startinfo;
                this.process.OutputDataReceived += new DataReceivedEventHandler(BufferOutput);
                this.process.Start();

                this.process.WaitForExit();


                return this.data;
            }
            else
            {
                throw new ApplicationException(this.label + " Not Initialized : Plugin must be initialzed prior to execution.");
            }
        }

        protected virtual void BufferOutput(object sender, DataReceivedEventArgs e)
        {
            this.output.Add(e.Data);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Mercenary.Interfaces
{
    public class ReportPluginTestHarness : TestHarness
    {
        [ImportMany]
        private IEnumerable<Lazy<ReportPlugin, PluginMetadata>> plugins;
        private ReportPlugin _plugin;

        public ReportPluginTestHarness(string pluginType) : base(pluginType) { }

        public ReportPluginTestHarness(string pluginDirectoryPath, string pluginType) : base(pluginDirectoryPath, pluginType) { }

        public ReportPluginTestHarness(Type pluginAssemblyType, string pluginType) : base(pluginAssemblyType, pluginType) { }

        public ReportPluginTestHarness(string pluginDirectoryPath, Type pluginAssemblyType, string pluginType) : base(pluginDirectoryPath, pluginAssemblyType, pluginType) { }

        public bool Discoverable()
        {
            return Discoverable(true);
        }

        public bool Discoverable(bool expectedResult)
        {
            var p = DiscoverPlugin();
            if (expectedResult)
            {
                return (Object.ReferenceEquals(p, null)) ? false : true;
            }
            else
            {
                return (Object.ReferenceEquals(p, null)) ? true : false;
            }
        }

        public bool Initializes(JObject parameters)
        {
            return Initializes(parameters, true);
        }

        public bool Initializes(JObject parameters, bool expectedResult)
        {
            var plugin = DiscoverPlugin();
            if (Object.ReferenceEquals(plugin, null))
            {
                throw new ApplicationException("Plugin " + this._pluginType + " not found");
            }
            else
            {
                var result = plugin.Initialize(parameters);
                return (expectedResult) ? result : !result;
            }
        }

        public bool Executes(JObject parameters)
        {
            return Executes(parameters, true);
        }

        public bool Executes(JObject parameters, bool expectedResult)
        {
            var plugin = DiscoverPlugin();
            if (Object.ReferenceEquals(plugin, null))
            {
                throw new ApplicationException("Plugin " + this._pluginType + " not found");
            }
            else
            {
                try
                {
                    var result = plugin.Execute(parameters);
                    return (expectedResult) ? result : !result;
                }
                catch
                {
                    return (expectedResult) ? false : true;
                }
            }
        }

        private void DoWork (object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private ReportPlugin DiscoverPlugin()
        {
            if (Object.ReferenceEquals(_plugin, null))
            {
                var p = plugins.Where(plugin => plugin.Metadata.Type.Equals(_pluginType)).DefaultIfEmpty(null).FirstOrDefault();
                _plugin = (Object.ReferenceEquals(p, null)) ? null : p.Value;
            }
            return _plugin;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Mercenary.Interfaces
{
    public class TaskPluginTestHarness : TestHarness
    {
        [ImportMany]
        private IEnumerable<Lazy<TaskPlugin, PluginMetadata>> plugins;
        private TaskPlugin _plugin;

        public TaskPluginTestHarness(string pluginType) : base(pluginType) { }

        public TaskPluginTestHarness(string pluginDirectoryPath, string pluginType) : base(pluginDirectoryPath, pluginType) { }

        public TaskPluginTestHarness(Type pluginAssemblyType, string pluginType) : base(pluginAssemblyType, pluginType) { }

        public TaskPluginTestHarness(string pluginDirectoryPath, Type pluginAssemblyType, string pluginType) : base(pluginDirectoryPath, pluginAssemblyType, pluginType) { }

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
            return Executes(parameters, null);
        }

        public bool Executes(JObject parameters, JObject expectedResult)
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
                    if (Object.ReferenceEquals(expectedResult, null))
                    {
                        return (result == null) ? true : false;
                    }
                    else
                    {
                        return (JToken.DeepEquals(expectedResult, result)) ? true : false;
                    }
                }
                catch
                {
                    return (Object.ReferenceEquals(expectedResult, null)) ? true : false;
                }
            }
        }

        private TaskPlugin DiscoverPlugin()
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

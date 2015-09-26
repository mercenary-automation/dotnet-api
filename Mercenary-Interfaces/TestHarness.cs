using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace Mercenary.Interfaces
{
    public abstract class TestHarness
    {
        protected CompositionContainer container;
        protected string _pluginType;

        public TestHarness(string pluginType) : this(null, null, pluginType) { }

        public TestHarness(string pluginDirectoryPath, string pluginType) : this(pluginDirectoryPath, null, pluginType) { }

        public TestHarness(Type pluginAssemblyType, string pluginType) : this(null, pluginAssemblyType, pluginType) { }

        public TestHarness(string pluginDirectoryPath, Type pluginAssemblyType, string pluginType)
        {
            var catalog = new AggregateCatalog();
            this._pluginType = pluginType;

            try
            {
                Assembly[] assemblies = new Assembly[]{ Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly() };
                foreach (Assembly assembly in assemblies)
                {
                    catalog.Catalogs.Add(new AssemblyCatalog(assembly));
                    catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(assembly.Location)));
                }

                if (!Object.ReferenceEquals(pluginAssemblyType, null))
                {
                    catalog.Catalogs.Add(new AssemblyCatalog(pluginAssemblyType.Assembly));
                }

                if (!Object.ReferenceEquals(pluginDirectoryPath, null))
                {
                    if (Directory.Exists(pluginDirectoryPath))
                    {
                        catalog.Catalogs.Add(new DirectoryCatalog(pluginDirectoryPath));
                    }
                }

                this.container = new CompositionContainer(catalog);
                this.container.ComposeParts(this);
            }
            catch (CompositionException cex)
            {
                Console.WriteLine(cex.ToString());
            }
        }
    }
}

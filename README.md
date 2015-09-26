Mercenary.Interfaces 
====================

![](https://raw.githubusercontent.com/mercenary-dotnet/mercenary-core/master/mercenary-dotnet.png)

**The Mercenary .NET API Current Version : 0.0.8**

Because we want it to be easy for you to take advantage of Mercenary - and to contribute back to the community - we've gone to the trouble of providing [Mercenary.Interfaces](https://www.nuget.org/packages/Mercenary.Interfaces/), the Mercenary .NET open API. Use it to kick off your awesome new  task and/or report plugins for the .NET implementation of [Mercenary](https://github.com/mercenary-automation/mercenary-specification).

## Using Mercenary.Interfaces ##

Follow these simple steps to begin making your own Mercenary plugins:

1. Create a new C# ([.NET Framework 4](http://msdn.microsoft.com/en-us/library/vstudio/w0x726c2(v=vs.100).aspx) or higher) project in [Visual Studio 2010](http://msdn.microsoft.com/en-us/library/dd831853(v=vs.100).aspx) (or higher).
2. Use the [NuGet](https://www.nuget.org/) Package Manager to install [Mercenary.Interfaces](https://www.nuget.org/packages/Mercenary.Interfaces/).
3. ????
4. [PROFIT!!!](http://knowyourmeme.com/memes/profit)

### Explanation of Files Added ###
When you use the NuGet Package Manager to install Mercenary.Interfaces, in addition to adding the required framework references to your project and resolving needed dependencies, it will also add three files into your default namespace, the purposes of which should be self evident based on their names.

- **ExampleReportPlugin.cs** : An example _report_ plugin with nothing implemented.  This is a good starting point, and it demonstrates the bare minimum needed to create a report plugin.

```csharp
using System;
using System.ComponentModel.Composition;
using Mercenary.Interfaces;
using Newtonsoft.Json.Linq;

namespace YourRootNamespace
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
```

- **ExampleTaskPlugin.cs** : An example _task_ plugin with nothing implemented.  This is a good starting point, and it demonstrates the bare minimum needed to create a task plugin.

```csharp
using System;
using System.ComponentModel.Composition;
using Mercenary.Interfaces;
using Newtonsoft.Json.Linq;

namespace YourRootNamespace
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
```

- **ExamplePluginTest.cs** : This is a simple static class demonstrating how to use the provided test harness classes to ensure your plugin will work as expected when dropped into the plugin folder of your Mercenary installation.

```csharp
using Mercenary.Interfaces;

namespace YourRootNamespace
{
    class ExamplePluginTest
    {
        public static string TestExamplePlugin()
        {
            var harness = new TaskPluginTestHarness("task-example");
            return (harness.Discoverable()) ? "Eureka!  It worked!" : "Curses!  Something foiled me!";
        }
    }
}
```

You can remove any or all of these files from your project, or rename them and use them; their only purpose is to assist you in getting started.

## Plugin : Configuration ##
In order for your plugin to be recognized by the target or server it is running on, it must be placed in the `[mercenary installation path]/plugins` folder AND there must be an entry in the plugin section of the [target or server configuration file](https://github.com/mercenary-automation/mercenary-specification/wiki/Configuration-Files).

Using the `ExampleTaskPlugin` class from above, my target configuration file would look like this:

```javascript
{
    "role" : "target",

    ...

    "plugins" :
    {
        "task-example" :
        {
            "my-key-1" : "my-value-1",
            "my-key-2" : "my-value-2"
        }
    }
}
```

When Mercenary starts up, it iterates over the entries in the plugin section of the configuration file and will only expose those that are (a) in the list, (b) are found in the plugins folder **AND** (c) return true when the `Initialize` method is called.

## Plugin : Initialization ##
For both tasks and reports, the [JObject](http://james.newtonking.com/json/help/index.html?topic=html/T_Newtonsoft_Json_Linq_JObject.htm) passed into the `Initialize` method will be the same-named section from the plugin section of the [target or server configuration file](https://github.com/mercenary-automation/mercenary-specification/wiki/Configuration-Files).

As it is the responsibility of the Initialize method to know what to do with the key-value pairs in a respective plugins section, the definitions of the keys and their values are completely up to the developer of the plugin, and can be as simple or complex as necessary.

***They should also be well documented on the plugin project site.***

The idea is to provide the plugin with details it may need (such as the local or remote paths to needed executables or repositories) to ensure it is even capable of executing a task, but without burdening the target or server with the need to do any validation on those details.

The `Initialize` method should only return true if it gets the information it needs and has verified that it is able to execute on data passed to it.  Any exceptions thrown will be interpreted by the target or server as an inability to execute, and the plugin will be removed from it's capabilities.

## Plugin : Execution ##
For both tasks and reports, a [JObject](http://james.newtonking.com/json/help/index.html?topic=html/T_Newtonsoft_Json_Linq_JObject.htm) is the only parameter passed into the `Execute` method.

### Task Plugins ###
In the case of task plugins, the JObject input parameter is the section of the task request payload that has the same name as the exported Type value on the plugin (using our example above, that would be the "task-example" section).

The return value of the Execute method should be the same JObject that was passed in, updated with pass/fail results of the task.

### Report Plugins ###
In the case of report plugins, the JObject input parameter is the entirety of the job request, each activity and task having been updated with the pass/fail results of the tasks.

The return value of the report plugin is a simple boolean value indicating success or failure of report execution, which is subsequently logged in the Mercenary installations event log.

## Support ##
If you have any issues with the Merecenary.Interfaces NuGet package or the API in general, please [check our wiki](https://github.com/mercenary-dotnet/mercenary-api/wiki) for possible solutions, or [log an issue](https://github.com/mercenary-dotnet/mercenary-api/issues).

If you'd like to contribute to this or any [Mercenary .NET](https://github.com/mercenary-dotnet) project, send an email to [mercenarydotnet@gmail.com](mailto:mercenarydotnet@gmail.com) expressing your desire and your qualifications.

## License ##
Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at [apache.org/licenses/LICENSE-2.0](http://www.apache.org/licenses/LICENSE-2.0)

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

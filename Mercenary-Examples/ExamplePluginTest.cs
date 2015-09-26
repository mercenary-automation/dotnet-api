using Mercenary.Interfaces;

namespace $rootnamespace$
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

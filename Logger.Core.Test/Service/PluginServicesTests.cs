using Moq;
using System;
using Xunit;

namespace Logger.Core.Test.Service
{
    public class PluginServicesTests
    {
        private PluginServices CreatePluginServices()
        {
            return new PluginServices(string.Empty);
        }

        [Fact]
        public void ExcecuteLog_StateUnderTest_ExpectedBehavior()
        {
            var unitUnderTest = this.CreatePluginServices();
            string type = "Error";
            string messege = "Test";

            unitUnderTest.ExcecuteLog(
                type,
                messege);

            Assert.True(true);
        }

        [Fact]
        public void LoadPlugins_StateUnderTest_ExpectedBehavior()
        {
            var unitUnderTest = this.CreatePluginServices();
            unitUnderTest.LoadPlugins();
            Assert.True(true);
        }
    }
}

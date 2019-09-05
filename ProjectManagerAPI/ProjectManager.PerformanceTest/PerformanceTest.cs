using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBench;
using ProjectManagerAPI.Controllers;


namespace ProjectManagerAPI.PerformanceTest
{
    [TestClass]
    public class PerformanceTest
    {
        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput,
       TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void PerformanceTests()
        {
            // Set up Prerequisites   
            var controllerObject = new ProjectController();
            // Act on Test  
            var response = controllerObject.RetrieveProjects();
            // Assert the result  
            Assert.IsTrue(response != null);
        }
    }
}

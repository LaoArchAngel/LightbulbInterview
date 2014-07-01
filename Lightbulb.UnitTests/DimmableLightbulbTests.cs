using System;
using LightbulbInterview;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lightbulb.UnitTests
{
    [TestClass]
    public class DimmableLightbulbTests
    {
        private class TestDimmable : DimmableLightbulbBase
        {
            public TestDimmable(int lumens, int wattage) : base(lumens, wattage)
            {
                
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void SetOutputWhenOutputIsZero_NewOutputIsZero_InvalidOperationException()
        {
            DimmableLightbulbBase bulb = new TestDimmable(500,10);
            bulb.SetOutput(0);
        }

        [TestMethod]
        public void SetOutputWhenOutputIsZero_NewOutputIsTen_SwitchCalled()
        {
            var mock = new Mock<DimmableLightbulbBase>(500, 10);
            mock.Setup(dlb => dlb.Switch()).Verifiable();
            DimmableLightbulbBase bulb = mock.Object;
            bulb.SetOutput(10);

            mock.Verify();
        }

        [TestMethod]
        public void SetOutputWhenOutputIsNotZero_NewOutputIsZero_SwitchCalled()
        {
            var mock = new Mock<DimmableLightbulbBase>(500, 10);
            mock.Setup(dlb => dlb.Switch()).Verifiable();
            DimmableLightbulbBase bulb = mock.Object;

            bulb.SetOutput(10);
            mock.Verify(dlb => dlb.Switch(), Times.AtMostOnce);

            bulb.SetOutput(0);
            mock.Verify(dlb => dlb.Switch(), Times.Exactly(2));
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void SetOutput_NewOutputIsSameAsOld_InvalidOperationException()
        {
            var mock = new Mock<DimmableLightbulbBase>(500, 10);
            mock.Setup(dlb => dlb.Switch()).Verifiable();
            DimmableLightbulbBase bulb = mock.Object;

            bulb.SetOutput(10);
            bulb.SetOutput(10);
        }
    }
}

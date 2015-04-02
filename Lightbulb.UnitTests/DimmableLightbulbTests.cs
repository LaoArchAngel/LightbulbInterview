using System;
using Moq;
using Xunit;

namespace LightbulbInterview.UnitTests
{
    public class DimmableLightbulbBaseTests
    {
        [Fact]
        public void SetOutputWhenOutputIsNotZero_SwitchNotCalled_InvalidOperationException()
        {
            var mock = new Mock<DimmableLightbulbBase>(500, 10);
            mock.Setup(dlb => dlb.Switch()).Verifiable();
            DimmableLightbulbBase bulb = mock.Object;

            bulb.SetOutput(10);
            bulb.SetOutput(15);

            mock.Verify(dlb => dlb.Switch(), Times.AtMostOnce);
        }

        [Fact]
        public void SetOutputWhenOutputIsZero_NewOutputIsNotZero_SwitchCalled()
        {
            var mock = new Mock<DimmableLightbulbBase>(500, 10);
            mock.Setup(dlb => dlb.Switch()).Verifiable();
            DimmableLightbulbBase bulb = mock.Object;
            bulb.SetOutput(10);

            mock.Verify();
        }

        [Fact]
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

        //[Fact, ExpectedException(typeof(InvalidOperationException))]
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

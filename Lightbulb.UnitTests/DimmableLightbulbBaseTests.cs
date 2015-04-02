using System;
using Moq;
using Xunit;

namespace LightbulbInterview.UnitTests
{
    public class DimmableLightbulbBaseTests
    {
        [Theory]
        [InlineData(10, 15), InlineData(5, 4), InlineData(1, 30)]
        public void SetOutput_WHEN_newOutput_and_currentOutput_greaterthan_zero_THEN_Switch_not_called(int currentOutput, int newOutput)
        {
            var mock = new Mock<DimmableLightbulbBase>(500, 10);
            mock.Setup(dlb => dlb.Switch()).Verifiable();
            DimmableLightbulbBase bulb = mock.Object;
            
            // Setup
            bulb.SetOutput(10);
            mock.ResetCalls();

            // Test
            bulb.SetOutput(15);

            mock.Verify(dlb => dlb.Switch(), Times.Never);
        }

        [Theory]
        [InlineData(10), InlineData(1), InlineData(13)]
        public void SetOutput_WHEN_current_output_equals_zero_and_newOutput_greaterthan_zero_THEN_Switch_called(int newOutput)
        {
            var mock = new Mock<DimmableLightbulbBase>(500, 10);
            mock.Setup(dlb => dlb.Switch()).Verifiable();
            DimmableLightbulbBase bulb = mock.Object;
            
            bulb.SetOutput(newOutput);

            mock.Verify();
        }

        [Theory]
        [InlineData(10), InlineData(1), InlineData(100)]
        public void SetOutput_Zero_WHEN_currentOutput_greaterthan_zero_THEN_Switch_called(int currentOutput)
        {
            var mock = new Mock<DimmableLightbulbBase>(500, 10);
            mock.Setup(dlb => dlb.Switch()).Verifiable();
            DimmableLightbulbBase bulb = mock.Object;

            // Setup
            bulb.SetOutput(currentOutput);
            mock.ResetCalls();

            // Test
            bulb.SetOutput(0);
            mock.Verify();
        }

        [Theory]
        [InlineData(5), InlineData(0), InlineData(1000)]
        public void SetOutput_WHEN_newOutput_equals_current_output_THEN_throws_InvalidOperationException(int newOutput)
        {
            var mock = new Mock<DimmableLightbulbBase>(500, 10);
            mock.Setup(dlb => dlb.Switch()).Verifiable();
            DimmableLightbulbBase bulb = mock.Object;

            if (newOutput > 0)
            {
                bulb.SetOutput(newOutput);
            }

            var ex = Record.Exception(() => bulb.SetOutput(newOutput));

            Assert.IsType<InvalidOperationException>(ex);
        }
    }
}

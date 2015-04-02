using System;
using System.Collections.Generic;
using Xunit;

namespace LightbulbInterview.UnitTests
{
    public class LightbulbBaseTests
    {
        private class TestBulb : LightbulbBase
        {
            public TestBulb(int lumens, int wattage) : base(lumens, wattage)
            {
                // THIS IS A UNIT-TESTING STUB ONLY! No changes should be make here.
            }
        }

        [Fact]
        public void Ctor_WHEN_Lumens_and_Wattage_nonzero_THEN_bulb_not_null()
        {
            const int lumens = 500;
            const int wattage = 8;

            LightbulbBase bulb = new TestBulb(lumens, wattage);

            Assert.NotNull(bulb);
        }

        [Theory]
        [InlineData(1), InlineData(500)]
        public void Ctor_WHEN_myLumens_greaterthan_zero_THEN_bulb_Lumens_equals_myLumens(int myLumens)
        {
            const int wattage = 5;

            LightbulbBase bulb = new TestBulb(myLumens, wattage);

            Assert.Equal(myLumens, bulb.Lumens);
        }

        [Theory]
        [InlineData(1), InlineData(500)]
        public void Ctor_WHEN_myWattage_greaterthan_zero_THEN_bulb_Wattage_equuals_myWattage(int myWattage)
        {
            const int lumens = 5;
            
            LightbulbBase bulb = new TestBulb(lumens, myWattage);

            Assert.Equal(myWattage, bulb.Wattage);
        }

        [Theory]
        [InlineData(0), InlineData(-1)]
        public void Ctor_WHEN_myLumens_not_greatherthan_zero_THEN_throws_InvalidOperationException(int myLumens)
        {
            const int wattage = 8;
            var actual = Record.Exception(() => new TestBulb(myLumens, wattage));

            Assert.IsType<InvalidOperationException>(actual);
        }

        [Theory]
        [InlineData(0), InlineData(-1)]
        public void Ctor_WHEN_myWattage_not_greaterthan_zero_THEN_throws_InvalidOperationException(int myWattage)
        {
            const int lumens = 5;
            var actual = Record.Exception(() => new TestBulb(lumens, myWattage));

            Assert.IsType<InvalidOperationException>(actual);
        }

        [Fact]
        public void Light_WHEN_Bulb_is_new_THEN_equals_zero()
        {
            const int lumens = 500;
            const int wattage = 10;

            LightbulbBase bulb = new TestBulb(lumens, wattage);

            Assert.Equal(0, bulb.Light);
        }

        [Theory]
        [InlineData(1), InlineData(500)]
        public void Light_WHEN_Switch_called_THEN_equals_Lumens(int myLumens)
        {
            const int wattage = 10;
            
            LightbulbBase bulb = new TestBulb(myLumens, wattage);

            bulb.Switch();

            Assert.Equal(myLumens, bulb.Light);
        }

        [Fact]
        public void Light_WHEN_Switch_called_twice_THEN_equals_zero()
        {
            const int lumens = 500;
            const int wattage = 10;
            
            LightbulbBase bulb = new TestBulb(lumens, wattage);

            bulb.Switch();
            bulb.Switch();

            Assert.Equal(0, bulb.Light);
        }

        [Theory]
        [MemberData("EnergyUsedData")]
        public void EnergyUsed_WHEN_timeOn_positive_THEN_equals_wattage_times_hoursOn_dividedby_1000(TimeSpan timeOn, int myWattage)
        {
            const int lumens = 500;

            var expected = timeOn.TotalHours * myWattage / 1000; // EnergyUsed read in KwH.
            LightbulbBase bulb = new TestBulb(lumens, myWattage);
            double actual = bulb.EnergyUsed(TimeSpan.Zero);
            
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> EnergyUsedData
        {
            get
            {
                return new[]
                {
                    new object[] {TimeSpan.FromHours(5), 5},
                    new object[] {TimeSpan.FromMinutes(35), 10},
                    new object[] {TimeSpan.FromMinutes(143), 1}
                };
            }
        }
        
        [Fact]
        public void EnergyUsed_TenWattageFiveHours_PointZeroFiveKwh()
        {
            const int wattage = 10;
            TimeSpan timeOn = TimeSpan.FromHours(5);
            double expected = wattage*timeOn.TotalHours/1000;

            LightbulbBase bulb = new TestBulb(500, wattage);

            double actual = bulb.EnergyUsed(timeOn);

            Assert.Equal(expected, actual);
        }
    }
}
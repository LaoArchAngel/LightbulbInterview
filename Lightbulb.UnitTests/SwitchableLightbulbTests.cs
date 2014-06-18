using System;
using LightbulbInterview;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lightbulb.UnitTests
{
    [TestClass]
    public class SwitchableLightbulbTests
    {
        [TestMethod]
        public void Ctor_ValidLumensAndWattage_Pass()
        {
            const int lumens = 500;
            const int wattage = 8;
            var moq = new Mock<LightbulbBase>(lumens, wattage);
            LightbulbBase bulb = moq.Object;

            Assert.IsNotNull(bulb);
            Assert.AreEqual(lumens, bulb.Lumens);
            Assert.AreEqual(wattage, bulb.Wattage);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Ctor_LumensZero_InvalidOperationException()
        {
            var moq = new Mock<LightbulbBase>(MockBehavior.Default, 0 , 8);
            LightbulbBase bulb = moq.Object;
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Ctor_WattageZero_InvalidOperationException()
        {
            var moq = new Mock<LightbulbBase>(500, 0);
            LightbulbBase bulb = moq.Object;
        }

        [TestMethod]
        public void New_LightIsZero_Pass()
        {
            var moq = new Mock<LightbulbBase>(500, 10);
            var bulb = moq.Object;

            Assert.AreEqual(0, bulb.Light);
        }

        [TestMethod]
        public void Switch_LightEqualsLumens_Pass()
        {
            const int lumens = 600;
            var moq = new Mock<LightbulbBase>(lumens, 10);
            var bulb = moq.Object;

            bulb.Switch();

            Assert.AreEqual(lumens, bulb.Light);
        }

        [TestMethod]
        public void SwitchTwice_LightIsZero_Pass()
        {
            var moq = new Mock<LightbulbBase>(500, 10);
            LightbulbBase bulb = moq.Object;

            bulb.Switch();
            bulb.Switch();

            Assert.AreEqual(0, bulb.Light);
        }

        [TestMethod]
        public void EnergyUsed_ZeroTimeSpan_ZeroEnergyUsed()
        {
            var moq = new Mock<LightbulbBase>(500, 10);
            LightbulbBase bulb = moq.Object;
            double energyUsed = bulb.EnergyUsed(TimeSpan.Zero);
            Assert.AreEqual(0, energyUsed);
        }

        [TestMethod]
        public void EnergyUsed_TenWattageFiveHours_PointZeroFiveKwh()
        {
            const int wattage = 10;
            var timeOn = TimeSpan.FromHours(5);
            double expected = wattage*timeOn.TotalHours/1000;

            var moq = new Mock<LightbulbBase>(500, wattage);
            LightbulbBase bulb = moq.Object;

            double actual = bulb.EnergyUsed(timeOn);

            Assert.AreEqual(expected, actual);
        }
    }
}

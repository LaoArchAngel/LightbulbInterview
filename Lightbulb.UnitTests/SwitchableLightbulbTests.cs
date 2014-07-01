using System;
using LightbulbInterview;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lightbulb.UnitTests
{
    [TestClass]
    public class SwitchableLightbulbTests
    {
        private class TestBulb : LightbulbBase
        {
            public TestBulb(int lumens, int wattage) : base(lumens, wattage)
            {
                
            }
        }

        [TestMethod]
        public void Ctor_ValidLumensAndWattage_Pass()
        {
            const int lumens = 500;
            const int wattage = 8;
            LightbulbBase bulb = new TestBulb(lumens, wattage);

            Assert.IsNotNull(bulb);
            Assert.AreEqual(lumens, bulb.Lumens);
            Assert.AreEqual(wattage, bulb.Wattage);
        }

        [TestMethod, ExpectedException(typeof (InvalidOperationException))]
        public void Ctor_LumensZero_InvalidOperationException()
        {
            new TestBulb(0, 8);
        }

        [TestMethod, ExpectedException(typeof (InvalidOperationException))]
        public void Ctor_WattageZero_InvalidOperationException()
        {
            new TestBulb(500, 0);
        }

        [TestMethod]
        public void New_LightIsZero_Pass()
        {
            LightbulbBase bulb = new TestBulb(500, 10);

            Assert.AreEqual(0, bulb.Light);
        }

        [TestMethod]
        public void Switch_LightEqualsLumens_Pass()
        {
            const int lumens = 600;
            LightbulbBase bulb = new TestBulb(lumens, 10);

            bulb.Switch();

            Assert.AreEqual(lumens, bulb.Light);
        }

        [TestMethod]
        public void SwitchTwice_LightIsZero_Pass()
        {

            LightbulbBase bulb = new TestBulb(500, 10);

            bulb.Switch();
            bulb.Switch();

            Assert.AreEqual(0, bulb.Light);
        }

        [TestMethod]
        public void EnergyUsed_ZeroTimeSpan_ZeroEnergyUsed()
        {
            LightbulbBase bulb = new TestBulb(500, 10);
            double energyUsed = bulb.EnergyUsed(TimeSpan.Zero);
            Assert.AreEqual(0, energyUsed);
        }

        [TestMethod]
        public void EnergyUsed_TenWattageFiveHours_PointZeroFiveKwh()
        {
            const int wattage = 10;
            TimeSpan timeOn = TimeSpan.FromHours(5);
            double expected = wattage*timeOn.TotalHours/1000;

            LightbulbBase bulb = new TestBulb(500, wattage);

            double actual = bulb.EnergyUsed(timeOn);

            Assert.AreEqual(expected, actual);
        }
    }
}
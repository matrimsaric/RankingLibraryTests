using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using RankingLibrary.CalculationEngine;
using RankingLibrary.SupportObjects.PlayerObjects;
using System.Numerics;

namespace RankingLibraryTests.EloTests
{
    [TestClass]
    public class EloTes
    {
        /// <summary>
        /// TODO test on unexpected ELOs ? less than 400 - upper level could be inifinite dependant on 
        /// pool of players.
        /// </summary>
        [TestMethod]
        public void TestEloExpectedWinner()
        {
            // create 2 Elos
            Elo p1Elo = new Elo(45, 1700.0, 34, DateTime.Now);
            Elo p2Elo = new Elo(100, 1300.0, 120, DateTime.Now);

            CalculateMaster calc = new EloRating();
            calc.GetRating(ref p1Elo, ref p2Elo, GAME_RESULT.WIN);

            Assert.IsNotNull(p1Elo);
            Assert.IsNotNull(p2Elo);

            // Test Expected band of change of winner
            if (p1Elo.EloValue < 1700)
            {
                Assert.Fail("ELO of winner has dropped");
            }
            if (p1Elo.EloValue > 1705)
            {
                Assert.Fail("ELO of winner has increased beyond expected variance");
            }
            if (p1Elo.DeviationValue > 34)
            {
                Assert.Fail("Deviation of winner has increased");
            }

            // Test Expected band of change of loser
            if (p2Elo.EloValue >= 1300)
            {
                Assert.Fail("ELO of loser has increased");
            }
            if (p2Elo.EloValue < 1295)
            {
                Assert.Fail("ELO of loser has decreased beyond expected variance");
            }
            if (p1Elo.DeviationValue > 120)
            {
                Assert.Fail("Deviation of loser has increased");
            }

        }

        [TestMethod]
        public void TestEloUnExpectedWinner()
        {
            // create 2 Elos
            Elo p1Elo = new Elo(45, 1700.0, 34, DateTime.Now);
            Elo p2Elo = new Elo(100, 1300.0, 120, DateTime.Now);

            CalculateMaster calc = new EloRating();
            calc.GetRating(ref p1Elo, ref p2Elo, GAME_RESULT.LOSE);

            Assert.IsNotNull(p1Elo);
            Assert.IsNotNull(p2Elo);

            // Test Expected band of change of winner
            if (p1Elo.EloValue > 1700)
            {
                Assert.Fail("ELO of loser has increased");
            }
            if (p1Elo.EloValue < 1670)
            {
                Assert.Fail("ELO of loser has decreased beyond expected variance");
            }
            if (p1Elo.DeviationValue < 34)
            {
                Assert.Fail("Deviation of loser has decreased");
            }

            // Test Expected band of change of loser
            if (p2Elo.EloValue <= 1300)
            {
                Assert.Fail("ELO of winner has decreased");
            }
            if (p2Elo.EloValue > 1330)
            {
                Assert.Fail("ELO of winner has increased beyond expected variance");
            }
            if (p2Elo.DeviationValue < 120)
            {
                Assert.Fail("Deviation of winner has decreased");
            }

        }

        [TestMethod]
        public void TestInvalidId()
        {
            try
            {
                Elo p1Elo = new Elo(-45, 1700.0, 34, DateTime.Now);
            }
            catch
            {
                return;
            }
            Assert.Fail("Exception not thrown when PlayerId invalid");

        }
        [TestMethod]
        public void TestInvalidEloValue()
        {
            try
            {
                Elo p1Elo = new Elo(12, -399, 34, DateTime.Now);
            }
            catch
            {
                return;
            }
            Assert.Fail("Exception not thrown when Elo Value invalid");

        }

        [TestMethod]
        public void TestInvalidDeviationValue()
        {
            try
            {
                Elo p1Elo = new Elo(12, 399, -34, DateTime.Now);
            }
            catch
            {
                return;
            }
            Assert.Fail("Exception not thrown when Deviation Value invalid");

        }
    }
}
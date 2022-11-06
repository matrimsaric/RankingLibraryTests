using RankingLibrary.CalculationEngine;
using RankingLibrary.SupportObjects.MatchObjects;
using RankingLibrary.SupportObjects.PlayerObjects;
using RankingLibrary.SupportObjects.RatingObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingLibraryTests.GlickoTests
{
    [TestClass]
    public class GlickoOps
    {
        private string hackText = @"<img src=x onerror=&#x22;&#x61;&#x6C;&#x65;&#x72;&#x74;&#x28;&#x31;&#x29;&#x22;>";
        private RatingCalculator calculator = new RatingCalculator();

        [TestMethod]
        public void TestInvalidId()
        {
            try
            {
                Rating p1 = new Rating(-45,  calculator, 2200, 200, 0.06);
            }
            catch
            {
                return;
            }
            Assert.Fail("Exception not thrown when PlayerId invalid");

        }
        //[TestMethod]
        //public void TestInvalidPlayerName()
        //{
        //    try
        //    {
        //        Rating p1 = new Rating(-45,  calculator, 2200, 200, 0.06);
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //    Assert.Fail("Exception not thrown when PlayerName hacked");

        //}

        [TestMethod]
        public void TestInvalidRating()
        {
            try
            {
                Rating p1 = new Rating(5,  calculator, -59, 200, 0.06);
            }
            catch
            {
                return;
            }
            Assert.Fail("Exception not thrown when Rating Invalid");

        }

        [TestMethod]
        public void TestInvalidDeviation()
        {
            try
            {
                Rating p1 = new Rating(5,  calculator, 500, -200, 0.06);
            }
            catch
            {
                return;
            }
            Assert.Fail("Exception not thrown when Deviation Invalid");

        }

        [TestMethod]
        public void TestInvalidVolatility()
        {
            try
            {
                Rating p1 = new Rating(5, calculator, 500, 200, -0.05);
            }
            catch
            {
                return;
            }
            Assert.Fail("Exception not thrown when Volatility Invalid");

        }

        [TestMethod]
        public void TestValidUser()
        {
            try
            {
                Rating p1 = new Rating(5,  calculator, 500, 200, 0.05);
            }
            catch
            {
                Assert.Fail("Exception thrown when User isvalid");
            }


        }

        [TestMethod]
        public void TestValidBestPlayerResults()
        {
            Rating p1 = new Rating(1,  calculator, 2200, 200, 0.06);
            Rating p2 = new Rating(2,  calculator);

            RatingPeriodResults results = new RatingPeriodResults();
            results.AddResult(p1, p2);

            calculator.UpdateRankings(results);

            // Test Expected band of change of winner
            if (p1.RatingValue < 2200)
            {
                Assert.Fail("ELO of winner has decreased");
            }
            if (p1.RatingValue > 2220)
            {
                Assert.Fail("ELO of winner has increased beyond expected variance");
            }
            if (p1.RatingDeviation > 200)
            {
                Assert.Fail("Deviation of winner has increased ");
            }
            if (p1.RatingDeviation < 196 || p1.RatingDeviation > 198)
            {
                Assert.Fail("Deviation of winner has adjusted beyond expected variance " + p1.RatingDeviation);
            }
            //if (p1.Volatility > 0.06)
            //{
            //    Assert.Fail("Volatility of winner has increased");
            //}

            // Test Expected band of change of loser
            if (p2.RatingValue > 1500)
            {
                Assert.Fail("ELO of loser has increased");
            }
            if (p2.RatingValue < 1336)
            {
                Assert.Fail("ELO of loser has decreased beyond expected variance");
            }
            if (p2.RatingDeviation < 289 || p2.RatingDeviation > 292)
            {
                Assert.Fail("Deviation of loser has adjusted beyond expected variance :" + p2.RatingDeviation);
            }
 
            //if (p2.Volatility < 0.06)
            //{
            //    Assert.Fail("Volatility of loser has decreased");
            //}


        }
    }
}

using NuGet.Frameworks;
using RankingLibrary.CalculationEngine;
using RankingLibrary.DataAccess;
using RankingLibrary.SupportObjects.PlayerObjects;
using RankingLibrary.SupportObjects.RatingObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RankingLibraryTests.GamerTests
{
    [TestClass]
    public class UserMaster
    {
        private string hackText = @"<img src=x onerror=&#x22;&#x61;&#x6C;&#x65;&#x72;&#x74;&#x28;&#x31;&#x29;&#x22;>";
        private RatingCalculator calculator = new RatingCalculator();
        private DataAccessProvider dataAccess = new DataAccessProvider();

        [TestMethod]
        public void TestInvalidUserId()
        {
            try
            {
                Player newPlayer = new Player(-22);
            }
            catch
            {
                return;
            }
            Assert.Fail("Player had an invalid id and should have thrown an execption");
            
        }

        [TestMethod]
        public void TestUserCreation()
        {
            try
            {
                Player pl = new Player(0, true);

                Rating p2 = new Rating(0, calculator);



                // Test for existence

                Task<Player> test = dataAccess.GetLiveDataAccess().LoadBasePlayer(0);

                if (test.Result != null)
                {
                    // now check rating data has saved
                    Task<DataTable> dat = dataAccess.GetLiveDataAccess().GetLiveRating(0);

                    if (dat.Result != null)
                    {
                        DataTable resp = dat.Result;

                        if (resp.Rows.Count != 1)
                        {
                            Assert.Fail("Test User Rating Data was not created correctly");
                        }
                        else
                        {
                            // user created now we want to delete the lot
                            Task<bool> delete = dataAccess.GetLiveDataAccess().DeletePlayer(0, true);

                            if (delete.Result != false)
                            {
                                // check for existence of Player. This time expect a fail
                                try
                                {
                                    Task<Player> testDelete = dataAccess.GetLiveDataAccess().LoadBasePlayer(0);
                                    Assert.Fail("User has not deleted!");
                                }
                                catch
                                {
                                    // finally check ratings table
                                    try
                                    {
                                        Task<DataTable> dat3 = dataAccess.GetLiveDataAccess().GetLiveRating(0);
                                        Assert.Fail("User Rating has not deleted!");
                                    }
                                    catch
                                    {
                                        return;
                                    }
                                    

                               
                                }
                                

                                if (dat.Result != null)
                                {
                                    


                                }
                                else
                                {
                                    
                                }
                            }
                        }

                    }
                    else
                    {
                        Assert.Fail("Test User was not created");
                    }

                }

            }
            catch(Exception exc)
            {
                Assert.Fail("Error " + exc.Message);
            }
        }

        //[TestMethod]
        //public void TestUserNameHack()
        //{
        //    try
        //    {
        //        Player newPlayer = new Player(-22);
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //    Assert.Fail("Player had hack text accepted..");

        //}





    }
}

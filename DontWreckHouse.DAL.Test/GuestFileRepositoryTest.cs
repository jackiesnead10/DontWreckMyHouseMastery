using NUnit.Framework;
using DontWreckHouse.Core.Models;
using DontWreckHouse.DAL;
using System.Collections.Generic;

namespace DontWreckHouse.DAL.Test
{
    public class GuestRepoTests
    {


        const string SEED_FILE_PATH = @"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckHouse.DAL.Test\Data\Guest-seed.csv";
        const string TEST_FILE_PATH = @"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckHouse.DAL.Test\Data\Guest-data-test\guest-file.csv";
        const string TEST_DIR_PATH = @"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckHouse.DAL.Test\Data\Guest-data-test\";
        GuestFileRepository repo = new GuestFileRepository(TEST_FILE_PATH);

        

        [SetUp]
        public void Setup()
        {
            

        }

        [Test]
        public void ShouldLoad()
        {
         
           List<Guest> all = repo.FindAll();

            Assert.AreEqual(2, all.Count);
        }
        [Test]
        public void ShouldReadByEmail()
        {
            Guest guest = repo.ViewByEmail("slomas0@mediafire.com");

            Assert.AreEqual(guest.Email, "slomas0@mediafire.com");
        }
    
    }
}
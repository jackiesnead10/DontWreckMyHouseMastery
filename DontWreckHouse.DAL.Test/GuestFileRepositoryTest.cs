using NUnit.Framework;
using DontWreckHouse.Core.Models;
using DontWreckHouse.DAL;
using System.Collections.Generic;
using DontWreckHouse.BLL.Test;

namespace DontWreckHouse.DAL.Test
{
    public class GuestRepoTests
    {


       const string SEED_FILE_PATH = @"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckHouse.DAL.Test\Data\Guest-seed.csv";
        const string TEST_FILE_PATH = @"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckHouse.DAL.Test\Data\Guest-data-test\guest-file.csv";
        const string TEST_DIR_PATH = @"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckHouse.DAL.Test\Data\Guest-data-test\";
        GuestFileRepositoryDouble repo;

        

        [SetUp]
        public void Setup()
        {
            repo = new GuestFileRepositoryDouble();

        }

        [Test]
        public void ShouldLoad()
        {
         
           List<Guest> all = repo.FindAll();

            Assert.AreEqual(1, all.Count);
        }
        [Test]
        public void ShouldReadByEmail()
        {
            Guest guest = repo.ViewByEmail("boat@boat.com");

            Assert.AreEqual(guest.Email, "boat@boat.com");
        }
    
    }
}
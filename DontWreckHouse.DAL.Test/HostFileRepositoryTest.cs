using DontWreckHouse.Core;
using DontWreckHouse.Core.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace DontWreckHouse.DAL.Test
{
    public class Tests
    {
        HostFileRepository repo;
        [SetUp]
        public void Setup()
        {
            ILogger logger = new FileLogger(@"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckMyHouse.UI\dont-wreck-my-house-data\Logs");
            repo = new HostFileRepository(@"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckHouse.DAL.Test\Data\Host-seed.csv", logger);
        }

        [Test]
        public void ShouldFindAll()
        {
           
            List<Host> all = repo.FindAll();
            Assert.AreEqual(2, all.Count);
        }
        [Test]
        public void ShouldFindByEmail()
         {
            
            Host hostId = repo.ViewByEmail("krhodes1@posterous.com");
            Assert.AreEqual(hostId.Email, "krhodes1@posterous.com");
         }
        
    
   
   }
}
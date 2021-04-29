using DontWreckHouse.Core.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace DontWreckHouse.DAL.Test
{
    public class Tests
    {
        HostFileRepository repo = new HostFileRepository(@"C:\Users\Jacqueline Snead\source\repos\DontWreckMyHouse\DontWreckHouse.DAL.Test\Data\Host-seed.csv");
        [SetUp]
        public void Setup()
        {
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
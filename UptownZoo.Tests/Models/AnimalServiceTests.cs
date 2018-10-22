using Microsoft.VisualStudio.TestTools.UnitTesting;
using UptownZoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;

namespace UptownZoo.Models.Tests {
    [TestClass()]
    public class AnimalServiceTests {
        private IQueryable<Animals> animals;

        [TestInitialize] // Runs before each test
        public void beforeTests() {
            animals = new List<Animals>() {
                new Animals(){AnimalID = 1, Species = "Zebra"},
                new Animals(){AnimalID = 2, Species = "Elephant"},
                new Animals(){AnimalID = 3, Species = "Hyena"}
            }.AsQueryable();
         }

        [TestMethod]
        public void GetAnimals_ShouldReturnAllAnimalsSortedBySpecies() {
            // Set up a Mock databse and Mock animal tables
            var mockAnimals = new Mock<DbSet<Animals>>();

            mockAnimals.As<IQueryable<Animals>>().Setup(m => m.Provider).Returns(animals.Provider);
        }
    }
}
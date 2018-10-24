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
            // Arrange
            Mock<DbSet<Animals>> mockAnimals = GetAnimalMockDBSet();

            //Create mock database
            var mockDB = new Mock<ApplicationDbContext>();
            mockDB.Setup(db => db.Animals).Returns(mockAnimals.Object);

            //Act
            IEnumerable<Animals> allAnimals = AnimalService.GetAllAnimals(mockDB.Object);

            //Assert all animals are returned
            Assert.AreEqual(3, allAnimals.Count());

            // Assert animals sorted by species
            Assert.AreEqual("Elephant", allAnimals.ElementAt(0).Species);
            Assert.AreEqual("Zebra", allAnimals.ElementAt(2).Species);
        }

        private Mock<DbSet<Animals>> GetAnimalMockDBSet() {
            var mockAnimals = new Mock<DbSet<Animals>>();

            mockAnimals.As<IQueryable<Animals>>().Setup(m => m.Provider).Returns(animals.Provider);
            mockAnimals.As<IQueryable<Animals>>().Setup(m => m.Expression).Returns(animals.Expression);
            mockAnimals.As<IQueryable<Animals>>().Setup(m => m.GetEnumerator()).Returns(animals.GetEnumerator());
            return mockAnimals;
        }
    }
}
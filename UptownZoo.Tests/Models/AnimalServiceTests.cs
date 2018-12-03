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
            Mock<DbSet<Animals>> mockAnimals = GetMockAnimalDBSet();

            //Create mock database
            var mockDB = GetMockDB(mockAnimals);

            //Act
            IEnumerable<Animals> allAnimals = AnimalService.GetAllAnimals(mockDB.Object);

            //Assert all animals are returned
            Assert.AreEqual(3, allAnimals.Count());

            // Assert animals sorted by species
            Assert.AreEqual("Elephant", allAnimals.ElementAt(0).Species);
            Assert.AreEqual("Zebra", allAnimals.ElementAt(2).Species);
        }

        [TestMethod]
        public void AddAnimal_NewAnimalShouldCallAddAndSaveChanges() {
            Mock<DbSet<Animals>> mockAnimals = GetMockAnimalDBSet();

            Mock<ApplicationDbContext> mockDB = GetMockDB(mockAnimals);

            Animals beast = new Animals() {
                Species = "Tiger"
            };

            AnimalService.AddAnimal(beast, mockDB.Object);

            // Make sure the AddAnimal method truly got called (only once) from AnimalService class
            mockAnimals.Verify(m => m.Add(beast), Times.Once);

            // Make sure animal was added to the mock database
            mockDB.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void AddAnimal_NullAnimal_ShouldThrowArgumentNullException() {
            Animals a = null; // Lol

            // Assert => Act
            Assert.ThrowsException<ArgumentNullException>(() => AnimalService.AddAnimal(a, new ApplicationDbContext())); // Empty parathesis for new method
        }

        private static Mock<ApplicationDbContext> GetMockDB(Mock<DbSet<Animals>> mockAnimals) {
            var mockDB = new Mock<ApplicationDbContext>();

            mockDB.Setup(db => db.Animals).Returns(mockAnimals.Object);
            return mockDB;
        }

        private Mock<DbSet<Animals>> GetMockAnimalDBSet() {
            var mockAnimals = new Mock<DbSet<Animals>>();

            mockAnimals.As<IQueryable<Animals>>().Setup(m => m.Provider).Returns(animals.Provider);
            mockAnimals.As<IQueryable<Animals>>().Setup(m => m.Expression).Returns(animals.Expression);
            mockAnimals.As<IQueryable<Animals>>().Setup(m => m.GetEnumerator()).Returns(animals.GetEnumerator());
            return mockAnimals;
        }
    }
}
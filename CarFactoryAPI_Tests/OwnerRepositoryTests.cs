using CarAPI.Entities;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryAPI_Tests
{
    public class OwnerRepositoryTests
    {
        Mock<FactoryContext> contextMock;
        OwnerRepository ownerRepository;
        public OwnerRepositoryTests()
        {
            // Create Mock of Dependencies
            contextMock = new();
            ownerRepository = new(contextMock.Object);
        }

        [Fact]
        [Trait("Author", "Heba")]
        public void AddOwner_AskForAllOwners_EmptyOwnerList()
        {
            // Arrange
            // Build the mock data
            List<Owner> owners = new List<Owner>() { };

            // setup called Dbsets
            contextMock.Setup(o => o.Owners).ReturnsDbSet(owners);

            // Act
            List<Owner> owner = ownerRepository.GetAllOwners();

            // Assert
            Assert.Empty(owner);
        }
        [Fact]
        [Trait("Author", "Heba")]
        public void AddOwner_AskForAllOwners_NotEmptyOwnerList()
        {
            // Arrange
            // Build the mock data
            List<Owner> owners = new List<Owner>()
    {
        new Owner { Id = 1, Name = "John Doe" } // Add a sample owner
    };

            // setup called Dbsets
            contextMock.Setup(o => o.Owners).ReturnsDbSet(owners);

            // Act
            List<Owner> owner = ownerRepository.GetAllOwners();

            // Assert
            Assert.NotEmpty(owner);
        }


        [Fact]
        [Trait("Author", "Heba")]
        public void AddOwner_AskForOwnerObject_True()
        {
            // Arrange
            //Build the mock data

            Owner owner = new Owner() { Id = 1, Name = "Heba", Car = new Car { Id = 1 } };
            List<Owner> owners = new List<Owner>();

            // setup called DBSets
            contextMock.Setup(o => o.Owners).ReturnsDbSet(owners);

            // Act
            bool result = ownerRepository.AddOwner(owner);

            // Assert
            Assert.True(result);
        }
        [Fact]
        [Trait("Author", "Heba")]
        public void AddOwner_AskForOwnerObject_False()
        {
            // Arrange
            //Build the mock data
            Owner owner = new Owner() { Id = 1, Name = "Heba", Car = new Car { Id = 1 } };
            List<Owner> owners = new List<Owner>();

            // Set up a scenario where adding the owner fails (e.g., owner already exists)
            contextMock.Setup(o => o.Owners.Add(It.IsAny<Owner>())).Throws(new InvalidOperationException());

            // setup called DBSets
            contextMock.Setup(o => o.Owners).ReturnsDbSet(owners);

            // Act
            bool result = ownerRepository.AddOwner(owner);

            // Assert
            Assert.False(result);
        }

    }
}

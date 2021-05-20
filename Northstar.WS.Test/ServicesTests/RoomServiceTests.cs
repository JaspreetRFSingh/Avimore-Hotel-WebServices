using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Northstar.WS.Models;
using Northstar.WS.Services;
using Northstar.WS.Test.Providers;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Test.ServicesTests
{
    [TestClass]
    public class RoomServiceTests
    {

        [TestMethod]
        public void GetRooms_fetches_all_rooms()
        {
            var data = new List<Room>
            {
                new Room { Name = "Deluxe",Rate = 2300 },
                new Room { Name = "ExeZ", Rate = 2500 },
                new Room { Name = "Basic", Rate = 2000 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Room>>();
            mockSet.As<IQueryable<Room>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Room>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Room>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Room>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AvimoreDBContext>();
            mockContext.Setup(c => c.Rooms).Returns(mockSet.Object);

            var service = new RoomService(mockContext.Object);
            List<Room> rooms = null;
            //service.GetRooms();

            Assert.AreEqual(3, rooms.Count);
            //order is changed because getAllRooms method returns the rooms based o alphabetical order
            Assert.AreEqual("Basic", rooms[0].Name);
            Assert.AreEqual("Deluxe", rooms[1].Name);
            Assert.AreEqual("ExeZ", rooms[2].Name);

        }

        [TestMethod]
        public void AddRoom_adds_a_room()
        {
            var mockSet = new Mock<DbSet<Room>>();

            var mockContext = new Mock<AvimoreDBContext>();
            mockContext.Setup(m => m.Rooms).Returns(mockSet.Object);

            var service = new RoomService(mockContext.Object);
            Room mockRoomToBeAdded = new Room { Name = "VVIP", Rate = 3400 };
            service.InsertRoom(mockRoomToBeAdded);

            mockSet.Verify(m => m.Add(It.IsAny<Room>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        // TODO: Yet to be rectified
        [TestMethod]
        public async Task GetRoomByIdAsync_fetches_a_room_by_id()
        {
            //ARRANGE
            var data = new List<Room>
            {
                new Room { RoomId = 101, Name = "Deluxe",Rate = 2300 },
                new Room { RoomId = 102, Name = "ExeZ", Rate = 2500 },
                new Room { RoomId = 103, Name = "Basic", Rate = 2000 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Room>>(); mockSet.As<IDbAsyncEnumerable<Room>>()
                 .Setup(m => m.GetAsyncEnumerator())
                 .Returns(new TestDbAsyncEnumerator<Room>(data.GetEnumerator()));

            mockSet.As<IQueryable<Room>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Room>(data.Provider));
            mockSet.As<IQueryable<Room>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Room>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Room>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AvimoreDBContext>();
            mockContext.Setup(c => c.Rooms).Returns(mockSet.Object);

            //ACT
            var service = new RoomService(mockContext.Object);
            var room = await service.GetRoomByIdAsync(101);
            

            //ASSERT
            Assert.AreEqual("Deluxe", room.Name);
            Assert.AreEqual(2300, room.Rate);
        }

    }
}

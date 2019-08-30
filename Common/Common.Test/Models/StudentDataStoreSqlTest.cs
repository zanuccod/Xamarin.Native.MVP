using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Common.Entities;
using Common.Models;

namespace Common.Test.Models
{
    [TestFixture]
    public class StudentDataStoreSqlTest
    {
        private StudentSqLiteDataStore db;
        private const string dbPath = "dbSqLiteTest";

        [SetUp]
        public void BeforeEachTest()
        {
            db = new StudentSqLiteDataStore(dbPath + ".db3");
        }

        [TearDown]
        public void AfterEachTest()
        {
            // delete all database files generated for test
            var files = Directory.GetFiles(Path.GetDirectoryName(Path.GetFullPath(dbPath)), dbPath + ".*");
            foreach (var file in files)
                File.Delete(file);
        }

        [Test]
        public void InsertOneElement_Succes()
        {
            var item = new Student() { Name = "name", BornDate = "01-01-1970", Country = "TEST" };
            Task.FromResult(db.AddItemAsync(item));

            Assert.AreEqual(1, db.GetItemsAsync().Result.Count);
        }

        [Test]
        public void UpdateElement_Succes()
        {
            var item = new Student() { Name = "name", BornDate = "01-01-1970", Country = "TEST" };
            Task.FromResult(db.AddItemAsync(item));

            // reload element to get item with given Id
            item = db.GetItemsAsync().Result.FirstOrDefault();
            item.Name = "name1";
            Task.FromResult(db.UpdateItemAsync(item));

            Assert.True(item.Equals(db.GetItemsAsync().Result.FirstOrDefault()));
        }

        [Test]
        public void DeleteElement_Success()
        {
            var item = new Student() { Name = "name", BornDate = "01-01-1970", Country = "TEST" };
            Task.FromResult(db.AddItemAsync(item));

            // reload element to get item with given Id
            item = db.GetItemsAsync().Result.FirstOrDefault();
            Task.FromResult(db.DeleteItemAsync(item));

            Assert.AreEqual(0, db.GetItemsAsync().Result.Count);
        }

        [Test]
        public void DeleteAllAsync_Success()
        {
            // Arrange
            var items = new List<Student>()
            {
                new Student() { Name = "name", BornDate = "01-01-1970", Country = "TEST" },
                new Student() { Name = "name1", BornDate = "01-01-1970", Country = "TEST" },
                new Student() { Name = "name2", BornDate = "01-01-1970", Country = "TEST" }
            };

            items.ForEach(x => Task.FromResult(db.AddItemAsync(x)));
            Assert.AreEqual(items.Count, db.GetItemsAsync().Result.Count);


            // Act
            Task.FromResult(db.DeleteAllAsync());

            // Assert
            Assert.AreEqual(0, db.GetItemsAsync().Result.Count);
        }
    }
}

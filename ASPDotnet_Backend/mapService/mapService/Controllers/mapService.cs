using System;
using mapService.Models;
using MongoDB.Driver;

namespace mapService.Controllers
{
    public class mapService
    {
        private readonly IMongoCollection<Branch> _branches;

        public mapService(mapServiceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _branches = database.GetCollection<Branch>(settings.BranchCollectionName);
        }

        public List<Book> Get() =>
            _branches.Find(book => true).ToList();

        public Book Get(string id) =>
            _branches.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _branches.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _branches.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _branches.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _branches.DeleteOne(book => book.Id == id);

    }
}

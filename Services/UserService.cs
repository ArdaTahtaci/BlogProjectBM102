using BlogProject.Entities;
using BlogProject.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace BlogProject.Services{
    public class UserService : IUserService {
        private readonly IMongoCollection<User> _users;

    public UserService(IDatabaseSettings settings, IMongoClient mongoClient){
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _users = database.GetCollection<User>(settings.UsersCollectionName);
    }
    public IEnumerable<User> GetAllUsers() => _users.Find(user => true).ToList();
    
    public User GetUserById(string id) => _users.Find<User>(user => user._id == id).FirstOrDefault();
    
    public User CreateUser(User user){
            _users.InsertOne(user);
            return user;
        }

    }

    public interface IUserService{
        IEnumerable<User> GetAllUsers();
        User GetUserById(string id);
        User CreateUser(User user);
    }

}

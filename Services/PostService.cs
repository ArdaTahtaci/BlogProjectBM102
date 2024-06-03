using BlogProject.Entities;
using BlogProject.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace BlogProject.Services{
    public class PostService : IPostService {
        private readonly IMongoCollection<Post> _posts;

    public PostService(IDatabaseSettings settings, IMongoClient mongoClient){
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _posts = database.GetCollection<Post>(settings.PostsCollectionName);
    }
    public IEnumerable<Post> GetAllPosts() => _posts.Find(post => true).ToList();
    
    
    public Post CreatePost(Post post){
            _posts.InsertOne(post);
            return post;
        }

    }

    public interface IPostService{
        IEnumerable<Post> GetAllPosts();
        Post CreatePost(Post post);
    }

}

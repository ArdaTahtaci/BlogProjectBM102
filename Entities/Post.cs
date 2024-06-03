namespace BlogProject.Entities{

    
    public class Post{
        public int _id {get; set;}
        public string PostId {get; set;}
        public User PostedBy {get; set;}
        public string Content {get; set;}
        public string Image {get; set;}
        public DateTime CreatedAt {get; set;}
        public Comment[] Comments {get; set;}
        public User[] Likes {get; set;}
    }

    public class Comment
    {
        public User CommentedBy { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
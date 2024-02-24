using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class PostRepository : IPostRepository
    {
        ApplicationDbContext Context;
        public PostRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }




        public Post GetById(int id)
        {
            Post onePost = Context.Posts.FirstOrDefault(s => s.Id == id);
            return onePost;
        }
        public List<Post> GetAll()
        {
            return Context.Posts.ToList();
        }

        public int Create(Post newPost)
        {
            Context.Posts.Add(newPost);
           
            int row = Context.SaveChanges();
            return row;
        }
        public int Update(int id, Post newPost)
        {
            Post OldPost = Context.Posts.FirstOrDefault(s => s.Id == id);
            OldPost.Content = newPost.Content;
            OldPost.Image = newPost.Image;

            int row = Context.SaveChanges();
            return row;
        }
        public int Delete(int id)
        {
            Post OldPost = Context.Posts.FirstOrDefault(s => s.Id == id);
            Context.Posts.Remove(OldPost);
            int row = Context.SaveChanges();
            return row;
        }
    }
}

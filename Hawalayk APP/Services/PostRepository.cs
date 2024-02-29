using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;
using Twilio.Rest.Taskrouter.V1.Workspace.TaskQueue;

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

        public async Task<Post> CreatNewPostAsync(Craftsman craftsman, PostDTO newPost)
        {
            var post = new Post
            {
                Content = newPost.Content,
                ImageURL = newPost.Image,
                CraftsmanId = craftsman.Id,
                CraftId = craftsman.CraftId


            };
            Context.Posts.Add(post);
           
            Context.SaveChanges();
            return new Post { Content = newPost.Content, ImageURL = newPost.Image };
        }
     


        public async Task<List<PostDTO>> GetCraftGallary(int craftId)
        {
            var posts = await Context.Posts
           .Include(p=>p.craft) 
           .Where(p => p.craft.Id == craftId)  
           .ToListAsync();
            return  posts.Select(p => new PostDTO { Content = p.Content, Image = p.ImageURL }).ToList();
        }
        public int Update(int id, Post newPost)
        {
            Post OldPost = Context.Posts.FirstOrDefault(s => s.Id == id);
            OldPost.ImageURL = newPost.ImageURL;
            OldPost.Content = newPost.Content;
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

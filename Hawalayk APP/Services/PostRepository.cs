using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using System.ComponentModel;
using System.Reflection;

namespace Hawalayk_APP.Services
{
    public class PostRepository : IPostRepository
    {
        ApplicationDbContext Context;
        private readonly ICraftsmenRepository craftsmanRepo;

        public PostRepository(ApplicationDbContext _Context, ICraftsmenRepository _craftsmanRepo)
        {
            Context = _Context;
            craftsmanRepo = _craftsmanRepo;
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
        public int Create(string craftsmanId, PostDTO postDTO)
        {
            Craftsman craftsman = craftsmanRepo.GetById(craftsmanId);
            PostStatus enumValue = (PostStatus)ConvertToEnum<PostStatus>(postDTO.Flag);


            var file = postDTO.imgFile;
            string fileName = file.FileName;
            string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
            using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }


            Post post = new Post()
            {
                ImageURL = fileName,
                Content = postDTO.Content,
                Flag = enumValue,
                CraftsmanId = craftsmanId,
                CraftId = craftsman.CraftId
            };
            Context.Posts.Add(post);
            int row = Context.SaveChanges();
            return row;


        }

        public List<Post> GetGrafGallary(int craftId) ////مش محتاج اسم الحرفة لكن محتاج الحرفى؟؟؟
        {
            List<Post> posts = Context.Posts.Where(s => s.CraftId == craftId && s.Flag == (Enums.PostStatus.Gallery | Enums.PostStatus.Both)).ToList();//حبيت اقارن بالاس مش نفع لان الاسم enum وانا ببعته string
            return posts;
        }

        public List<Post> GetGraftsmanPortfolio(string craftsmanId) ////مش محتاج اسم الحرفة لكن محتاج الحرفى؟؟؟
        {
            List<Post> posts = Context.Posts.Where(s => s.CraftsmanId == craftsmanId && s.Flag == (Enums.PostStatus.Portfolio | Enums.PostStatus.Both)).ToList();//حبيت اقارن بالاس مش نفع لان الاسم enum وانا ببعته string
            return posts;
        }

        private static T? ConvertToEnum<T>(string arabicString) where T : struct
        {
            Type enumType = typeof(T);

            if (enumType.IsEnum)
            {
                foreach (FieldInfo field in enumType.GetFields())
                {
                    if (Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                    {
                        if (attribute.Description == arabicString)
                        {
                            return (T)field.GetValue(null);
                        }
                    }
                }
            }
            return null;
        }

    }
}

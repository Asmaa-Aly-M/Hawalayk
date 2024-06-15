using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace Hawalayk_APP.Services
{
    public class PostRepository : IPostRepository
    {
        ApplicationDbContext Context;
        private readonly ICraftsmenRepository _craftsmanRepository;
        private readonly ICraftRepository _craftRepository;
        private readonly IFileService _fileService;
        public PostRepository(ApplicationDbContext _Context, ICraftsmenRepository craftsmanRepository, 
            ICraftRepository craftRepository, IFileService fileService)
        {
            Context = _Context;
            _craftsmanRepository = craftsmanRepository;
            _craftRepository = craftRepository;
            _fileService = fileService;
        }

        public async Task<Post> GetById(int id)
        {
            Post onePost = await Context.Posts.FirstOrDefaultAsync(s => s.Id == id);
            return onePost;
        }
        public async Task<List<Post>> GetAll()
        {
            return await Context.Posts.ToListAsync();
        }

        public async Task<int> Update(int id, PostUpdatedDTO postUpdateDto)
        {
            Post post = await GetById(id);
            if (post == null) return -1;
            if (!string.IsNullOrWhiteSpace(postUpdateDto.Content))
            {
                post.Content = postUpdateDto.Content;
            }

            if (!string.IsNullOrWhiteSpace(postUpdateDto.Flag))
            {
                if (Enum.TryParse<PostStatus>(postUpdateDto.Flag, out var enumValue))
                {
                    post.Flag = enumValue;
                }
                else
                {
                    throw new ArgumentException("Invalid flag value");
                }
            }

            var postImagePath = await _fileService.SaveFileAsync(postUpdateDto.imgFile, "PostImages");
            post.ImageURL = postImagePath;

            Context.Posts.Update(post);
            int row = await Context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Delete(int id)
        {
            Post OldPost = await Context.Posts.FirstOrDefaultAsync(s => s.Id == id);
            Context.Posts.Remove(OldPost);
            int row = await Context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Create(string craftsmanId, PostDTO postDTO)
        {
            Craftsman craftsman = await _craftsmanRepository.GetById(craftsmanId);
            //PostStatus enumValue = (PostStatus)ConvertToEnum<PostStatus>(postDTO.Flag);
            PostStatus enumValue = (PostStatus)Enum.Parse(typeof(PostStatus), postDTO.Flag);

            var postImagePath = await _fileService.SaveFileAsync(postDTO.imgFile, "PostImages");

            Post post = new Post()
            {
                ImageURL = postImagePath,
                Content = postDTO.Content,
                Flag = enumValue,
                //Flag=PostStatus.Gallery, 
                CraftsmanId = craftsmanId,
                CraftId = craftsman.CraftId
            };
            Context.Posts.Add(post);
            int row = await Context.SaveChangesAsync();
            return row;

        }

        public async Task<List<GallaryPostDTO>> GetGrafGallary(string craftName)
        {
            Craft craft = null;
            CraftName enumValue = await _craftRepository.GetEnumValueOfACraftByArabicDesCription(craftName);

            craft = await Context.Crafts.FirstOrDefaultAsync(c => c.Name == enumValue);
            if (craft == null)
            {
                return null;
            }
            List<Post> posts = await Context.Posts.Include(c => c.Craftsman).Where(s => s.CraftId == craft.Id &&
            (s.Flag == Enums.PostStatus.Gallery | s.Flag == Enums.PostStatus.Both)).ToListAsync();

            // Convert Post objects to PostDTO objects
            List<GallaryPostDTO> postDTOs = posts.Select(post =>
            {
                return new GallaryPostDTO
                {
                    PostId = post.Id,
                    PostImgUrl = post.ImageURL,// Construct image URL
                    CraftsmanId = post.Craftsman.Id,
                    CraftsmanName = post.Craftsman.UserName,
                    Content = post.Content,
                    CraftsmanProfilePicUrl = Path.Combine("imgs/", post.Craftsman.ProfilePicture),
                    CraftName = craftName,
                    // Assuming Flag is an enum, convert it to string
                };
            }).ToList();

            return postDTOs;






        }

        //public List<Post> GetGrafGallary(string craftName)
        //{
        //    Craft craft = null;
        //    CraftName enumValue = (CraftName)ConvertToEnum<CraftName>(craftName);

        //    craft = Context.Crafts.FirstOrDefault(c => c.Name == enumValue);
        //    List<Post> posts = Context.Posts.Where(s => s.CraftId == craft.Id && s.Flag == (Enums.PostStatus.Gallery | Enums.PostStatus.Both)).ToList();//حبيت اقارن بالاس مش نفع لان الاسم enum وانا ببعته string
        //    return posts;
        //}

        public async Task<List<GallaryPostDTO>> GetGraftsmanPortfolio(string craftsmanId)
        {

            List<Post> posts = await Context.Posts.Where(s => s.CraftsmanId == craftsmanId &&
            (s.Flag == Enums.PostStatus.Portfolio | s.Flag == Enums.PostStatus.Both)).ToListAsync();//حبيت اقارن بالاس مش نفع لان الاسم enum وانا ببعته string
            Craftsman craftsman = await _craftsmanRepository.GetById(craftsmanId);

            var enumValue = (CraftName)craftsman.Craft.Name;

            var descriptionAttributes = typeof(CraftName)
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                as DescriptionAttribute[];

            var craftName = descriptionAttributes?.Length > 0 ? descriptionAttributes[0].Description : "Description not found.";


            List<GallaryPostDTO> postDTOs = posts.Select(post =>
            {
                return new GallaryPostDTO
                {
                    PostId = post.Id,
                    PostImgUrl = post.ImageURL,// Construct image URL
                    CraftsmanId = craftsman.Id,
                    CraftsmanName = post.Craftsman.UserName,
                    Content = post.Content,
                    CraftsmanProfilePicUrl = craftsman.ProfilePicture,
                    CraftName = craftName,
                    // Assuming Flag is an enum, convert it to string
                };
            }).ToList();

            return postDTOs;

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

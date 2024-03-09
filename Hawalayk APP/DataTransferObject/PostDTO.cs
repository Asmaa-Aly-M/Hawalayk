using Hawalayk_APP.Enums;

namespace Hawalayk_APP.DataTransferObject
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string ImageURL { get; set; }

        public string? Content { get; set; }

        public PostStatus Flag { get; set; }
    }
}

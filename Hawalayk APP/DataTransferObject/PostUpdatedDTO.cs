using System.ComponentModel.DataAnnotations.Schema;

namespace Hawalayk_APP.DataTransferObject
{
    public class PostUpdatedDTO
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? imgFile { get; set; }
        //  public string ImageURL { get; set; }

        public string? Content { get; set; }

        public string? Flag { get; set; }
    }
}

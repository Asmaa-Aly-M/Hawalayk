namespace Hawalayk_APP.DataTransferObject
{
    public class ReviewDTO
    {//ايه الي محتاجينه بالظبط في ال dto?
    
        public int Id { get; set; }

        public int Rating { get; set; }
        public string Content { get; set; }
        public int PositiveReacts { get; set; }
        public int NegativeReacts { get; set; }
    }
}

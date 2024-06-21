namespace Hawalayk_APP.DataTransferObject
{
    public class ShowReviewDTO
    {
        // isLiked = false ;
        //isUnlided =
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public int? PositiveReacts { get; set; }
        public int? NegativeReacts { get; set; }
        public string CustomerFristName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerProfileImage { get; set; }
    }
}

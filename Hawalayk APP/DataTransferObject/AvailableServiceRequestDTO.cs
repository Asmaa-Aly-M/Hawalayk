﻿namespace Hawalayk_APP.DataTransferObject
{
    public class AvailableServiceRequestDTO
    {
        public string CustomerID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerProfilePicture { get; set; }
        public string Content { get; set; }
        public string? OptionalImage { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
<<<<<<< HEAD:Hawalayk APP/DataTransferObject/AvailableServiceRequestDTO.cs
        public DateTime Date { get; set; } = DateTime.Now;
=======
        public int ServiceRequestId { get; set; }
        public DateTime DatePosted { get; set; }
>>>>>>> 27c68d1993d377eee2dd232c3420b1ed436779cf:Hawalayk APP/DataTransferObject/ServiceNeededRepalyDTO.cs
    }
}

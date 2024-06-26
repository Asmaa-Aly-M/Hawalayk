﻿using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAsync();
        Task<Address> GetByIdAsync(int id);
        Task<Address> CreateAsync(string governorateName, string cityName, string streetName);
    }
}

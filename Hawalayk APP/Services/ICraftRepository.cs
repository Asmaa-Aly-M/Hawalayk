﻿using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICraftRepository
    {
        Task<List<string>> GetAllCraftsNamesAsync();
        Task<Craft> GetOrCreateCraftAsync(string craftName);
        Task<List<CraftsmanDTO>> GetCraftsmenOfACraft(CraftName craftName);

        int Create(Craft newCraft);
        int Delete(int id);
        List<Craft> GetAll();
        Craft GetById(int id);
        int Update(int id, Craft newCraft);
    }
}
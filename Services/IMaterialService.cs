
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Model;
using Newtonsoft.Json.Linq;

namespace App.Services
{
    public interface IMaterialService
    {
        //Task<string> AddAsync(string material);
        Task<Material> AddAsync(Material material);
        Task<JObject> AddAsync(JObject material);

        

    }
}
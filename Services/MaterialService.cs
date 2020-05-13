using App.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Model;
using Newtonsoft.Json.Linq;

namespace App.Services
{
    public class MaterialService : IMaterialService
    {
        private static readonly string ContainerName = "materials";
        private readonly ICosmosService _cosmosService;
        private readonly Container _container;

        public MaterialService(ICosmosService cosmosService)
        {
            _cosmosService = cosmosService;
            _container = _cosmosService.GetDatabase().GetContainer(ContainerName);
        }

        public async Task<JObject> AddAsync(JObject material)
        {
            ItemResponse<JObject> response = null;
            try
            {
                response = await this._container
                    .CreateItemAsync<JObject>(material, new PartitionKey(material.GetValue("sourceSystemId").ToString()));
            }
            catch (CosmosException ex)
            {

            }
            return response;
        }
    }
}
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace App.Services
{
    public interface IMaterialService
    {
        Task<JObject> AddAsync(JObject material);
    }
}
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorApp6.Client.Services.MatterialService
{
    public class MatterialService : IMetterialService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public List<Matterial> matterials { get; set; }
        public IEnumerable<MaterialsItem> matsItem { get; set; }

        public MatterialService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task CreateMetterialAsync(Matterial item)
        {
            var res = await _httpClient.PostAsJsonAsync("api/Matterials", item);
            if (res == null)
                throw new Exception("not posted");
            _navigationManager.NavigateTo("matList");
        }

        public async Task DeleteMetterialAsync(int id)
        {
            var res = await _httpClient.DeleteAsync($"api/Matterials/{id}");
            if (res == null)
                throw new Exception("not deleted");
            _navigationManager.NavigateTo("matList");
        }

        public async Task<Matterial> GetMetterialAsync(int id)
        {
            var res = await _httpClient.GetFromJsonAsync<Matterial>($"api/Matterials/{id}");
            if (res != null)
            {
                return res;
            }
            throw new Exception("not found");
        }

        public async Task GetMetterialsAsync()
        {
            var res = await _httpClient.GetFromJsonAsync<List<Matterial>>("api/Matterials");
            if (res == null)
            {
                throw new Exception("not found");
            }
            matterials = res;
        }

        public async Task GetMetterialsOfItemAsync(int id)
        {
            await GetMetterialsAsync();
            var res = await _httpClient.GetFromJsonAsync<List<MaterialsItem>>($"api/MaterialsItems/Item/{id}");
            if (res == null)
                throw new Exception("not found");
            matterials = matterials.Where(o => res.Contains(o.Idmat);

        }

        public async Task UpdateMetterialAsync(Matterial item)
        {
            var res = await _httpClient.PutAsJsonAsync($"api/Matterials/{item.Idmat}", item);
            if (res == null)
                throw new Exception("not posted");
            _navigationManager.NavigateTo("matList");
        }
    }
}

using BlazorApp6.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorApp6.Client.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public ItemService(HttpClient httpClient,NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public List<Item> items { get; set; } = new List<Item>();

        public async Task CreateItemAsync(Item item)
        {
            var res = await _httpClient.PostAsJsonAsync("api/Items", item);
            if (res == null)
                throw new Exception("not posted");
            _navigationManager.NavigateTo("itemList");
        }

        public async Task DeleteItemAsync(int id)
        {
            var res = await _httpClient.DeleteAsync($"api/Items/{id}");
            if (res == null)
                throw new Exception("not deleted");
            _navigationManager.NavigateTo("itemList");
        }

        public async Task<Item> GetItemAsync(int id)
        {
            var res = await _httpClient.GetFromJsonAsync<Item>($"api/Items/{id}");
            if (res != null)
            {
                return res;
            }
            throw new Exception("not found");
        }

        public async Task GetItemsAsync()
        {
            var res = await _httpClient.GetFromJsonAsync<List<Item>>("api/Items");
            if (res == null)
            {
                throw new Exception("not found");
            }
            items = res;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var res = await _httpClient.PutAsJsonAsync($"api/Items/{item.Iditem}", item);
            if (res == null)
                throw new Exception("not posted");
            _navigationManager.NavigateTo("itemList");
        }

    }
}
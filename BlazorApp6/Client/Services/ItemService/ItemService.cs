using System.Net.Http.Json;

namespace BlazorApp6.Client.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _httpClient;

        public ItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Item> items { get; set; } = new List<Item>();
        public List<ItemsGroup> itemsGroups { get; set; } = new List<ItemsGroup>();
        public List<MaterialsItem> materialItems { get; set; } = new List<MaterialsItem>();

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
                return;
            }
            items = res;
        }
    }
}
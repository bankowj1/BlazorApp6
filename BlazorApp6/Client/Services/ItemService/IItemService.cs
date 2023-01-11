namespace BlazorApp6.Client.Services.ItemService
{
    public interface IItemService
    {
        List<Item> items { get; set; }

        Task<Item> GetItemAsync(int id);

        Task UpdateItemAsync(Item item);

        Task CreateItemAsync(Item item);

        Task DeleteItemAsync(int id);

        Task GetItemsAsync();
    }
}
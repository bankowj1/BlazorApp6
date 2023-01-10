namespace BlazorApp6.Client.Services.ItemService
{
    public interface IItemService
    {
        List<Item> items { get; set; }
        List<ItemsGroup> itemsGroups { get; set; }
        List<MaterialsItem> materialItems { get; set; }

        Task<Item> GetItemAsync(int id);

        Task GetItemsAsync();
    }
}
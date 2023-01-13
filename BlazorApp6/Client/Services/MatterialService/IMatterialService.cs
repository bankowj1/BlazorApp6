namespace BlazorApp6.Client.Services.MatterialService
{
    public interface IMatterialService
    {
        List<Matterial> matterials { get; set; }
        List<Matterial> matsItem { get; set; }
        Task<Matterial> GetMetterialAsync(int id);

        Task UpdateMetterialAsync(Matterial item);

        Task CreateMetterialAsync(Matterial item);

        Task DeleteMetterialAsync(int id);

        Task GetMetterialsOfItemAsync(int id);

        Task GetMetterialsAsync();
    }
}

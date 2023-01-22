using BlazorApp6.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorApp6.Client.Services.NoteService
{
    public class NoteService : INoteService
    {
        public List<Note> notes { get; set; }

        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public NoteService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task CreateNoteAsync(NoteDTO noteDTO)
        {
            var res = await _httpClient.PostAsJsonAsync("api/Notes", noteDTO);
            if (res == null)
                throw new Exception("not posted");
            _navigationManager.NavigateTo("notesList");
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            var res = await _httpClient.DeleteAsync($"api/Notes/{noteId}");
            if (res == null)
                throw new Exception("not deleted");
            _navigationManager.NavigateTo("itemList");
        }

        public async Task GetNotesAsync()
        {
            var res = await _httpClient.GetFromJsonAsync<List<Note>>("api/Notes/my");
            if (res != null)
            {
                throw new Exception("not found");
            }
            notes = res;
        }

        /*public Task<NoteDTO> GetNoteAsync(int id)
        {
            NoteDTO noteDTO = new NoteDTO();
            var not = notes.FirstOrDefault(e => e.Idnotes == id);
            if(not == null)
                throw new Exception("error");
            noteDTO.Note1 = "";
        }*/

        public Task UpdateNoteAsync(NoteDTO note, int id)
        {
            throw new NotImplementedException();
        }
    }
}
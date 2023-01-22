using BlazorApp6.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp6.Client.Services.NoteService
{
    public class NoteService : INoteService
    {
        public List<Note> notes { get; set; } = new List<Note>();

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
            _navigationManager.NavigateTo("notesList");
        }

        public async Task GetNotesAsync()
        {
            var res = await _httpClient.GetFromJsonAsync<List<Note>>("api/Notes/My");
            if (res == null)
            {
                throw new Exception("not found");
            }
            notes = res;
        }

        public Note GetNote(int id)
        {
            var not = notes.FirstOrDefault(e => e.Idnotes == id);
            if(not == null)
                throw new Exception("error");
            return not;
        }

        public async Task UpdateNoteAsync(NoteDTO note, int id)
        {
            var res = await _httpClient.PutAsJsonAsync($"api/Notes/{id}", note);
            if (res == null)
                throw new Exception("not posted");
            _navigationManager.NavigateTo("notesList");
        }

        public async Task <string> EncodeNote(NoteDTO note, string pass)
        {
            NoteDTOEnco noteDTOEnco = new NoteDTOEnco() { Note1 = note.Note1, pass = pass };
            var res = await _httpClient.PostAsJsonAsync("api/Notes/encode", noteDTOEnco);
            if(res != null)
            {
                var resp = await res.Content.ReadAsStringAsync();
                return resp;
            }
            return "";
        }


        public async Task<string> DecodeNote(NoteDTO note, string pass)
        {
            NoteDTOEnco noteDTOEnco = new NoteDTOEnco() { Note1 = note.Note1, pass = pass };
            var res = await _httpClient.PostAsJsonAsync("api/Notes/decode", noteDTOEnco);
            if (res != null)
            {
                var resp = await res.Content.ReadAsStringAsync();
                return resp;
            }
            return "";
        }
    }
}
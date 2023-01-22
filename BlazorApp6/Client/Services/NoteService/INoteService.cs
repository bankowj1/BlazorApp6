namespace BlazorApp6.Client.Services.NoteService
{
    public interface INoteService
    {
        List<Note> notes { get; set; }

        //Task<NoteDTO> GetNoteAsync(int id);

        Task CreateNoteAsync(NoteDTO noteDTO);

        Task UpdateNoteAsync(NoteDTO note,int id);

        Task DeleteNoteAsync(int noteId);

        Task GetNotesAsync();
    }
}
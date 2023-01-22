namespace BlazorApp6.Client.Services.NoteService
{
    public interface INoteService
    {
        List<Note> notes { get; set; }

        Note GetNote(int id);

        Task CreateNoteAsync(NoteDTO noteDTO);

        Task UpdateNoteAsync(NoteDTO note,int id);

        Task DeleteNoteAsync(int noteId);

        Task GetNotesAsync();

        Task<string> EncodeNote(NoteDTO note, string pass);
        Task<string> DecodeNote(NoteDTO note, string pass);
    }
}
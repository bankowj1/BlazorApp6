namespace BlazorApp6.Client.Services.NoteService
{
    public interface INoteService
    {
        List<Note> notes { get; set; }

        NoteDTO GetNote(int id);

        Task CreateNoteAsync(NoteDTO noteDTO);

        Task UpdateNoteAsync(NoteDTO note,int id);

        Task DeleteNoteAsync(int noteId);

        Task GetNotesAsync();

        NoteDTO EncodeNote(NoteDTO note, string pass);
        NoteDTO DecodeNote(NoteDTO note, string pass);
    }
}
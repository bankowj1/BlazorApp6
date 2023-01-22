namespace BlazorApp6.Client.Services.NoteService
{
    public interface INoteService
    {
        List<Note> notes { get; set; }

        Note GetNote(int id);

        Task CreateNoteAsync(NoteDTO noteDTO);

        Task UpdateNoteAsync(NoteDTO note,int id);
        Task UpdateNoteByteAsync(NoteDTOBytes note, int id);

        Task AddUserNote(int id, string username);

        Task DeleteNoteAsync(int noteId);

        Task GetNotesAsync();

        Task<byte[]> EncodeNote(NoteDTO note, string pass);
        Task<string> DecodeNote(Note note, string pass);
    }
}
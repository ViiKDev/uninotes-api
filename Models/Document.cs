namespace UniNotesAPI.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        // public string? Title { get; set; }
        // public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public int? FolderId { get; set; }
        public Folder? Folder { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
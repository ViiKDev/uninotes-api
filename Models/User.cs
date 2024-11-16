namespace UniNotesAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public ICollection<Folder>? Folders { get; set; }
        public ICollection<Document>? Documents { get; set; }
    }
}
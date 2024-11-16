namespace UniNotesAPI.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Document>? Documents { get; set; } = new List<Document>();
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
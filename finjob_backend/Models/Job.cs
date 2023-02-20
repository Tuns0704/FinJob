namespace finjob_backend.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public string Requirement { get; set; }
        public string Location { get; set; }
        public string Benefits { get; set; }
    }
}

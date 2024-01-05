namespace DiagnosticWebAPIServer.Model
{
    public class MedicalDirectoryItem
    {
        public int Id { get; set; }
        public string NameDisease { get; set; }
        public string Article { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}

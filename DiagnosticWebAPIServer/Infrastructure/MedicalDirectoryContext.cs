using Microsoft.EntityFrameworkCore;
using DiagnosticWebAPIServer.Model;

namespace DiagnosticWebAPIServer.Infrastructure
{
    public class MedicalDirectoryContext : DbContext
    {
        public MedicalDirectoryContext(DbContextOptions<MedicalDirectoryContext> options) : base(options)
        {
        }
        public DbSet<MedicalDirectoryItem> MedicalDirectory { get; set; }
        public DbSet<Source> Source { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace DiagnosticWebAPIServer.Infrastructure.Repositories
{
    public class MedicalDirectoryRepository
    {
        private readonly MedicalDirectoryContext _context;
        public MedicalDirectoryRepository(MedicalDirectoryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<string>> SearchForDiseasesBySymptomAsync(string symptom)
        {
            if (!string.IsNullOrWhiteSpace(symptom))
                return await _context.MedicalDirectory.Where(x => x.Article.Contains(symptom)).Select(y => y.NameDisease).ToListAsync();
            else
                return new List<string>();
        }

        public async Task<List<string>> SearchForDiseasesAsync(string disease)
        {
            if (!string.IsNullOrWhiteSpace(disease))
                return await _context.MedicalDirectory.Where(x => x.NameDisease.Contains(disease)).Select(y => y.NameDisease).ToListAsync();
            else
                return new List<string>();
        }

        public async Task<string> SearchForDiseasesByNameAsync(string disease)
        {
            if (!string.IsNullOrWhiteSpace(disease))
            {
                var medicalDirectoryItem = await _context.MedicalDirectory.FirstOrDefaultAsync(x => x.NameDisease == disease);
                return medicalDirectoryItem != null ? medicalDirectoryItem.Article : string.Empty;
            }
            else
                return string.Empty;
        }
    }
}

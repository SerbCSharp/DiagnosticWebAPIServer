using DiagnosticWebAPIServer.Infrastructure.Repositories;
using DiagnosticWebAPIServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace DiagnosticWebAPIServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DiagnosticController : ControllerBase
    {
        private readonly MedicalDirectoryRepository _medicalDirectoryRepository;
        public DiagnosticController(MedicalDirectoryRepository medicalDirectoryRepository)
        {
            _medicalDirectoryRepository = medicalDirectoryRepository ?? throw new ArgumentNullException(nameof(medicalDirectoryRepository));
        }

        [Route("SearchForDiseasesBySymptom")]
        [HttpGet]
        public async Task<IActionResult> SearchForDiseasesBySymptomAsync(string message)
        {
            var diseasesDto = new List<DiseaseDto>();
            if (message != null)
            {
                var diseases = new List<string>();
                var symptoms = message.Contains(',') ? message.Split(',') : message.Split(' ');

                foreach (var symptom in symptoms)
                {
                    var diseasesOnOneSymptom = await _medicalDirectoryRepository.SearchForDiseasesBySymptomAsync(symptom.Trim());
                    if (diseasesOnOneSymptom.Count > 0)
                        diseases.AddRange(diseasesOnOneSymptom);
                    var diseasesByName = await _medicalDirectoryRepository.SearchForDiseasesAsync(symptom.Trim());
                    if (diseasesByName.Count > 0)
                        diseases.AddRange(diseasesByName);
                }
                if (diseases.Count() > 0)
                {
                    diseasesDto = diseases.GroupBy(x => x).Select(g => new DiseaseDto { NameDisease = g.Key, Count = g.Count() }).OrderByDescending(y => y.Count).Take(20).ToList();
                }
            }
            return Ok(diseasesDto);
        }

        [Route("SearchForDiseasesByName")]
        [HttpGet]
        public async Task<IActionResult> SearchForDiseasesByNameAsync(string disease)
        {
            var article = await _medicalDirectoryRepository.SearchForDiseasesByNameAsync(disease);
            return Ok(article);
        }
    }
}

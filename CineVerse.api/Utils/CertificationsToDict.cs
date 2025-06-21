using CineVerse.api.Entities;
using CineVerse.shared.ApiResponses;

namespace CineVerse.api.Utils;

public class CertificationsToDict
{
    public static MovieCertificationsApiResponse MapEntitiesToDto(IEnumerable<CertificationEntity> rows)
    {
        var dict = rows.GroupBy(r => r.CountryCode.Trim().ToUpperInvariant())
                   .ToDictionary(
                       g => g.Key,            
                       g => g.Select(r => new CertificationApiResponse
                       {
                           Certification = r.Certification,
                           Meaning = r.Meaning ?? "",
                           Order = r.DisplayOrder
                       })
                            .OrderBy(c => c.Order)
                            .ToList(),
                       StringComparer.OrdinalIgnoreCase); 

        return new MovieCertificationsApiResponse { Certifications = dict };
    }

}

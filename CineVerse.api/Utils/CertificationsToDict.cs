using CineVerse.api.ApiResponses;
using CineVerse.api.Entities;

namespace CineVerse.api.Utils;

public class CertificationsToDict
{
    public static MovieCertificationsApiResponse MapEntitiesToDto(IEnumerable<CertificationEntity> rows)
    {
        var dict = rows.GroupBy(r => r.CountryCode)
                       .ToDictionary(
                           g => g.Key,
                           g => g.Select(r => new CertificationApiResponse
                           {
                               Certification = r.Certification,
                               Meaning = r.Meaning ?? "",
                               Order = r.DisplayOrder
                           })
                           .OrderBy(c => c.Order)
                           .ToList());

        return new MovieCertificationsApiResponse { Certifications = dict };
    }

}

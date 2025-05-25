namespace CineVerse.api.Entities;

public class CertificationEntity
{
    public string CountryCode { get; set; } = string.Empty;  
    public string Certification { get; set; } = string.Empty; 
    public string? Meaning { get; set; }
    public int DisplayOrder { get; set; }

    public CertificationEntity() { }
    public CertificationEntity(string c_c, string c, string m, int d_o)
    {
        CountryCode = c_c;
        Certification = c;
        Meaning = m;
        DisplayOrder = d_o;
    }
}

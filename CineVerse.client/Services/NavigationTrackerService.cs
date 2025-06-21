namespace CineVerse.client.Services;

public class NavigationTrackerService
{
    public string? PreviousPage { get; private set; }
    public string? CurrentPage { get; private set; }

    public void Update(string newPage)
    {
        PreviousPage = CurrentPage;
        CurrentPage = newPage;
    }
}

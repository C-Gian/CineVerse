namespace CineVerse.Client.Utils;

public static class QueueExtensions
{
    public static void EnqueueRange<T>(this Queue<T> q, IEnumerable<T> items)
    {
        foreach (var item in items)
            q.Enqueue(item);
    }

    public static List<T> DequeueChunk<T>(this Queue<T> q, int count)
    {
        var list = new List<T>(count);
        while (count-- > 0 && q.Count > 0)
            list.Add(q.Dequeue());
        return list;
    }
}

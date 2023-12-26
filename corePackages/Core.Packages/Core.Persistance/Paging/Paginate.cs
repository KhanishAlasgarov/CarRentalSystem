namespace Core.Persistance.Paging;

public class Paginate<T>
{
    public Paginate()
    {
        Items= new List<T>();
    }
    public int Size { get; set; }
    public int Index { get; set; }
    public int Count { get; set; }
    public int Pages { get; set; }
    public List<T> Items { get; set; }
    public bool HasPrevious => Index > 0;
    public bool HasNext => Index+1 < Pages;
}

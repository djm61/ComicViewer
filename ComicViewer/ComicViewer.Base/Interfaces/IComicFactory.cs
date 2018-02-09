namespace ComicViewer.Base.Interfaces
{
    public interface IComicFactory<T> where T : class
    {
        T Build();
    }
}

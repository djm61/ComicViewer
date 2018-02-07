namespace ComicViewer.Base.Interfaces
{
    public interface IComic
    {
        double BytesToMegaBytes(long bytes);

        void GenerateCover();

        string GetPage(int page);
    }
}

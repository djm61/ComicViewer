using System;

namespace ComicViewer.Base.Interfaces
{
    public interface IComic : IDisposable
    {
        double BytesToMegaBytes(long bytes);

        void GenerateCover();

        string GetPage(int page);

        void Dispose(bool disposing);
    }
}

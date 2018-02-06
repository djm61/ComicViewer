using System.IO;

namespace ComicViewer.Base
{
    public class CbrComic : Comic
    {
        public CbrComic(string path)
            : base(path)
        {
            GenerateCover();
        }

        public override void GenerateCover()
        {
            //todo fill in code
        }
    }
}

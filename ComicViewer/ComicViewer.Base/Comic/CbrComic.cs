using System.IO;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Readers;

namespace ComicViewer.Base.Comic
{
    public class CbrComic : Comic
    {
        public CbrComic(string path)
            : base(path)
        {
        }

        public override void GenerateCover()
        {
            ErrorMessage = string.Empty;
            var path = string.Empty;

            using (var rarFile = RarArchive.Open(FilePath))
            {
                PageCount = rarFile.Entries.Count;

                path = Path.GetTempPath();

                var firstEntry = rarFile.Entries.FirstOrDefault(e => !e.IsDirectory);
                if (firstEntry != null)
                {
                    if (File.Exists($"{path}\\{firstEntry.Key}"))
                    {
                        var fi = new FileInfo(path);
                        CoverPath = fi.FullName;
                    }
                    else
                    {
                        firstEntry.WriteToDirectory(path, new ExtractionOptions { Overwrite = true });
                        var fi = new FileInfo($"{path}\\{firstEntry.Key}");
                        CoverPath = fi.FullName;
                    }
                }
            }
        }

        public override string GetPage(int page)
        {
            //todo fill in code
            return string.Empty;
        }
    }
}

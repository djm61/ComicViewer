using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace ComicViewer.Base.Comic
{
    public class CbzComic : Comic
    {
        public CbzComic(string path)
            : base(path)
        {
        }

        public override void GenerateCover()
        {
            ErrorMessage = string.Empty;

            using (var zFile = new ZipFile(File.OpenRead(FilePath)))
            {
                PageCount = (int)zFile.Count;
            }

            using (var s = new ZipInputStream(File.OpenRead(FilePath)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    var directoryName = Path.GetDirectoryName(theEntry.Name);
                    var fileName = Path.GetFileName(theEntry.Name);

                    if (!string.IsNullOrWhiteSpace(directoryName) && directoryName.Length > 0)
                    {
                        var fDirectory = Path.GetTempPath() + directoryName;

                        if (Directory.Exists(fDirectory))
                        {
                            Directory.Delete(fDirectory, true);
                        }
                        Directory.CreateDirectory(fDirectory);
                    }

                    var fullPath = Path.GetTempPath() + theEntry.Name;

                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        if (File.Exists(fullPath))
                        {
                            CoverPath = fullPath;
                            return;
                        }

                        using (var streamWriter = File.Create(fullPath))
                        {
                            var data = new byte[2048];
                            while (true)
                            {
                                var size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            CoverPath = fullPath;
                            return;
                        }
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

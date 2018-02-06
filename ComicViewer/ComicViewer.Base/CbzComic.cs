using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace ComicViewer.Base
{
    public class CbzComic : Comic
    {
        public CbzComic(string path)
            : base(path)
        {
            GenerateCover();
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

                    // create directory if the archive has a folder at root
                    if (!string.IsNullOrWhiteSpace(directoryName) && directoryName.Length > 0)
                    {
                        var fDirectory = Path.GetTempPath() + directoryName;

                        //We need to delete the directory is it's already there so we get the first entry
                        if (Directory.Exists(fDirectory))
                        {
                            Directory.Delete(fDirectory, true);
                        }
                        Directory.CreateDirectory(fDirectory);
                    }

                    var fullPath = Path.GetTempPath() + theEntry.Name;

                    if (fileName != string.Empty && !File.Exists(fullPath))
                    {
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
                            return; //Just return the 1st entry in the archive
                        }
                    }
                }
            }
        }
    }
}

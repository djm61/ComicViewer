﻿using System.IO;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace ComicViewer.Base.Comic
{
    public class CbrComic : Comic
    {
        private IReader _reader;

        public CbrComic(string path)
            : base(path)
        {
        }

        public override void GenerateCover()
        {
            ErrorMessage = string.Empty;

            using (var rarFile = RarArchive.Open(FilePath))
            {
                PageCount = rarFile.Entries.Count;

                if (rarFile.IsSolid)
                {
                    //extract all entries
                    _reader = rarFile.ExtractAllEntries();
                    _reader.WriteAllToDirectory(TempPath, new ExtractionOptions { Overwrite = true, PreserveFileTime = true });

                    var file = Directory.GetFiles(TempPath)
                        .Select(fi => new FileInfo(fi))
                        .FirstOrDefault();

                    if (file == null)
                    {
                        throw new ComicException($"File is missing in {TempPath}");
                    }

                    CoverPath = file.FullName;
                }
                else
                {
                    var firstEntry = rarFile.Entries.FirstOrDefault(e => !e.IsDirectory);
                    if (firstEntry != null)
                    {
                        if (File.Exists($"{TempPath}\\{firstEntry.Key}"))
                        {
                            var file = Path.Combine(TempPath, firstEntry.Key);
                            var fi = new FileInfo(file);
                            CoverPath = fi.FullName;
                        }
                        else
                        {
                            firstEntry.WriteToDirectory(TempPath, new ExtractionOptions { Overwrite = true });
                            var fi = new FileInfo($"{TempPath}\\{firstEntry.Key}");
                            CoverPath = fi.FullName;
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

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _reader?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override void Dispose()
        {
            Dispose(true);
        }
    }
}

using System;
using System.IO;
using ComicViewer.Base.Interfaces;

namespace ComicViewer.Base.Comic
{
    public abstract class Comic : IComic
    {
        private const string ComicViewerName = "ComicViewer";
        public const int FirstPage = 0;

        private FileInfo _file;

        /// <summary>
        /// 
        /// </summary>
        protected string TempPath
        {
            get
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                path = Path.Combine(path, ComicViewerName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, FileNameNoExtension);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get { return _file.Name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileNameNoExtension
        {
            get { return _file.Name.Replace(_file.Extension, string.Empty); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FilePath
        {
            get { return _file.FullName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CoverPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double FileSize
        {
            get { return BytesToMegaBytes(_file.Length); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private FileInfo ComicFile
        {
            set { _file = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }

        protected Comic(string path)
        {
            ComicFile = new FileInfo(path);
        }

        public double BytesToMegaBytes(long bytes)
        {
            var b = Convert.ToDouble(bytes);
            var usedBw = b / (1024 * 1024);
            usedBw = Math.Round(usedBw, 3);
            return usedBw;
        }

        public abstract void GenerateCover();

        public abstract string GetPage(int page);

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

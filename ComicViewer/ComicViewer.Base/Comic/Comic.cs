using System;
using System.IO;
using ComicViewer.Base.Interfaces;

namespace ComicViewer.Base.Comic
{
    public abstract class Comic : IComic
    {
        public const int FirstPage = 0;

        private FileInfo _file;

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get
            {
                return _file.Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FilePath
        {
            get
            {
                return _file.FullName;
            }
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
            get
            {
                return BytesToMegaBytes(_file.Length);
            }
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
            set
            {
                _file = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected string ErrorMessage { get; set; }

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
    }
}

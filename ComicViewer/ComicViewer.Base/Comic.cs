using System;
using System.IO;
using ComicViewer.Base.Interfaces;

namespace ComicViewer.Base
{
    public abstract class Comic : IComic
    {
        private string _coverPath;
        private string _errorMessage;
        private int _pageCount;
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
        public string CoverPath
        {
            get
            {
                return _coverPath;
            }
            set
            {
                _coverPath = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long FileSize
        {
            get
            {
                return (long)BytesToMegaBytes(_file.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                _pageCount = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileInfo ComicFile
        {
            set
            {
                _file = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            }
        }

        public Comic(string path)
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

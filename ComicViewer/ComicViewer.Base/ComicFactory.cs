using System;
using System.IO;
using ComicViewer.Base.Comic;
using ComicViewer.Base.Interfaces;

namespace ComicViewer.Base
{
    public class ComicFactory : IComicFactory<Comic.Comic>
    {
        private const string CbzExtension = ".cbz";
        private const string CbrExtension = ".cbr";

        private readonly string _fileName;
        private readonly string _extension;

        public ComicFactory(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ComicException($"FileName is null or empty or whitespace");
            }

            _fileName = fileName;

            try
            {
                var fi = new FileInfo(fileName);
                _extension = fi.Extension;
            }
            catch (Exception ex)
            {
                throw new ComicException($"Could not get extension of {fileName}.", ex);
            }
        }

        public Comic.Comic Build()
        {
            Comic.Comic result;
            switch (_extension)
            {
                case CbzExtension:
                    result = new CbzComic(_fileName);
                    break;
                case CbrExtension:
                    result = new CbrComic(_fileName);
                    break;
                default:
                    throw new ComicException($"Invalid comic type for extension {_extension}");
            }

            return result;
        }
    }
}

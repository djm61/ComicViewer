using System.IO;
using ComicViewer.Base;
using ComicViewer.Base.Comic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComicViewer.Tests
{
    [TestClass]
    public class CbrComicTest
    {
        private string _path;
        private ComicFactory _factory;
        private Comic _comic;

        [TestInitialize]
        public void Setup()
        {
            _path = Path.Combine("ComicFiles", "comic.cbr");

            _factory = new ComicFactory(_path);
            _comic = _factory.Build();
        }

        [TestMethod]
        public void InitialComic_Default_ValidOutput()
        {
            Assert.IsNotNull(_comic);
            
            _comic.GenerateCover();

            var cover = _comic.CoverPath;
            Assert.IsFalse(string.IsNullOrWhiteSpace(cover));

            var pages = _comic.PageCount;
            Assert.IsTrue(pages > 0);

            var fileName = _comic.FileName;
            Assert.IsFalse(string.IsNullOrWhiteSpace(fileName));

            var filePath = _comic.FilePath;
            Assert.IsFalse(string.IsNullOrWhiteSpace(filePath));

            var size = _comic.FileSize;
            Assert.IsTrue(size > 0);
        }
    }
}

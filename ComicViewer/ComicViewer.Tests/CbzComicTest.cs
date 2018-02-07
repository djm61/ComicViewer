using System.IO;
using ComicViewer.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComicViewer.Tests
{
    [TestClass]
    public class CbzComicTest
    {
        private string _path;

        [TestInitialize]
        public void Setup()
        {
            _path = Path.Combine("ComicFiles", "comic.cbz");
        }

        [TestMethod]
        public void InitialComic_Default_ValidOutput()
        {
            var cbz = new CbzComic(_path);
            Assert.IsNotNull(cbz);

            var cover = cbz.CoverPath;
            Assert.IsFalse(string.IsNullOrWhiteSpace(cover));

            var pages = cbz.PageCount;
            Assert.IsTrue(pages > 0);
        }
    }
}

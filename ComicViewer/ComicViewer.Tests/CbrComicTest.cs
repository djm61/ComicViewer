using System.IO;
using ComicViewer.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComicViewer.Tests
{
    [TestClass]
    public class CbrComicTest
    {
        private string _path;

        [TestInitialize]
        public void Setup()
        {
            _path = Path.Combine("ComicFiles", "comic.cbr");
        }

        [TestMethod]
        public void InitialComic_Default_ValidOutput()
        {
            var cbr = new CbrComic(_path);
            Assert.IsNotNull(cbr);

            var cover = cbr.CoverPath;
            Assert.IsFalse(string.IsNullOrWhiteSpace(cover));

            var pages = cbr.PageCount;
            Assert.IsTrue(pages > 0);
        }
    }
}

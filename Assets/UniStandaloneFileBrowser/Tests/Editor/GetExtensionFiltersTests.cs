using NUnit.Framework;

namespace USFB.Tests
{
    public class GetExtensionsFiltersTests
    {
        [Test]
        public void NullInput_ReturnsEmptyArray()
        {
            var result = StandaloneFileBrowser.GetExtensionFilters(null);
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void EmptyString_ReturnsEmptyArray()
        {
            var result = StandaloneFileBrowser.GetExtensionFilters("");
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void SingleExtension_ParsesCorrectly()
        {
            var result = StandaloneFileBrowser.GetExtensionFilters("png");
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("PNG", result[0].Name);
            CollectionAssert.AreEqual(new[] { "png" }, result[0].Extensions);
        }

        [Test]
        public void MultipleExtensions_ParsesCorrectly()
        {
            var result = StandaloneFileBrowser.GetExtensionFilters("png,jpg,gif");
            Assert.AreEqual(3, result.Length);

            Assert.AreEqual("PNG", result[0].Name);
            CollectionAssert.AreEqual(new[] { "png" }, result[0].Extensions);

            Assert.AreEqual("JPG", result[1].Name);
            CollectionAssert.AreEqual(new[] { "jpg" }, result[1].Extensions);

            Assert.AreEqual("GIF", result[2].Name);
            CollectionAssert.AreEqual(new[] { "gif" }, result[2].Extensions);
        }

        [Test]
        public void ExtraWhitespace_IsTrimmed()
        {
            var result = StandaloneFileBrowser.GetExtensionFilters("  png  ,   jpg   ");
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("PNG", result[0].Name);
            CollectionAssert.AreEqual(new[] { "png" }, result[0].Extensions);
            Assert.AreEqual("JPG", result[1].Name);
            CollectionAssert.AreEqual(new[] { "jpg" }, result[1].Extensions);
        }

        [Test]
        public void MixedCaseNormalization()
        {
            var result = StandaloneFileBrowser.GetExtensionFilters("Png,JPg");
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("PNG", result[0].Name);
            CollectionAssert.AreEqual(new[] { "png" }, result[0].Extensions);
            Assert.AreEqual("JPG", result[1].Name);
            CollectionAssert.AreEqual(new[] { "jpg" }, result[1].Extensions);
        }
    }
}
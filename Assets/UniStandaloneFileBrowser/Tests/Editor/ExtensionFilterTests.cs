using System;
using NUnit.Framework;

namespace USFB.Tests
{
    /// <summary>
    /// Essential unit tests for the ExtensionFilter struct.
    /// Tests constructors and basic functionality.
    /// </summary>
    [TestFixture]
    public class ExtensionFilterTests
    {
        #region Constructor Tests

        /// <summary>
        /// Tests ExtensionFilter constructor with single extension.
        /// </summary>
        [Test]
        public void Constructor_WithSingleExtension_CreatesValidFilter()
        {
            // Arrange
            const string name = "Text Files";
            const string extension = "txt";

            // Act
            var filter = new ExtensionFilter(name, extension);

            // Assert
            Assert.AreEqual(name, filter.Name, "Filter name should match constructor parameter");
            Assert.IsNotNull(filter.Extensions, "Extensions array should not be null");
            Assert.AreEqual(1, filter.Extensions.Length, "Extensions array should contain one element");
            Assert.AreEqual(extension, filter.Extensions[0], "Extension should match constructor parameter");
        }

        /// <summary>
        /// Tests ExtensionFilter constructor with multiple extensions.
        /// </summary>
        [Test]
        public void Constructor_WithMultipleExtensions_CreatesValidFilter()
        {
            // Arrange
            const string name = "Image Files";
            var extensions = new[] { "png", "jpg", "gif" };

            // Act
            var filter = new ExtensionFilter(name, extensions);

            // Assert
            Assert.AreEqual(name, filter.Name, "Filter name should match constructor parameter");
            Assert.IsNotNull(filter.Extensions, "Extensions array should not be null");
            Assert.AreEqual(extensions.Length, filter.Extensions.Length, "Extensions array length should match input");
            
            for (int i = 0; i < extensions.Length; i++)
            {
                Assert.AreEqual(extensions[i], filter.Extensions[i], $"Extension at index {i} should match input");
            }
        }

        /// <summary>
        /// Tests ExtensionFilter constructor with null name parameter.
        /// </summary>
        [Test]
        public void Constructor_WithNullName_CreatesFilterWithNullName()
        {
            // Arrange
            const string name = null;
            const string extension = "txt";

            // Act
            var filter = new ExtensionFilter(name, extension);

            // Assert
            Assert.IsNull(filter.Name, "Filter name should be null when constructor parameter is null");
            Assert.IsNotNull(filter.Extensions, "Extensions array should not be null");
            Assert.AreEqual(1, filter.Extensions.Length, "Extensions array should contain one element");
            Assert.AreEqual(extension, filter.Extensions[0], "Extension should match constructor parameter");
        }

        /// <summary>
        /// Tests ExtensionFilter constructor with null extensions array.
        /// </summary>
        [Test]
        public void Constructor_WithNullExtensionsArray_CreatesFilterWithEmptyExtensions()
        {
            // Arrange
            const string name = "All Files";

            // Act
            var filter = new ExtensionFilter(name, null);

            // Assert
            Assert.AreEqual(name, filter.Name, "Filter name should match constructor parameter");
            Assert.IsNotNull(filter.Extensions, "Extensions array should not be null even when input is null");
            Assert.AreEqual(0, filter.Extensions.Length, "Extensions array should be empty when input is null");
        }

        /// <summary>
        /// Tests ExtensionFilter constructor with empty extensions array.
        /// </summary>
        [Test]
        public void Constructor_WithEmptyExtensionsArray_CreatesFilterWithEmptyExtensions()
        {
            // Arrange
            const string name = "All Files";
            var extensions = Array.Empty<string>();

            // Act
            var filter = new ExtensionFilter(name, extensions);

            // Assert
            Assert.AreEqual(name, filter.Name, "Filter name should match constructor parameter");
            Assert.IsNotNull(filter.Extensions, "Extensions array should not be null");
            Assert.AreEqual(0, filter.Extensions.Length, "Extensions array should be empty when input is empty");
        }
        
        [TestCase("p ng")]
        [TestCase("*gif")]
        [TestCase(".jp#g")]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("png", "jp g")]
        public void InvalidExtension_ThrowsException(params string[] invalidExtensions)
        {
            Assert.Throws<ArgumentException>(() =>
                _ = new ExtensionFilter("Invalid", invalidExtensions)
            );
        }

        #endregion

        #region Property Tests

        /// <summary>
        /// Tests that Name property can be accessed correctly.
        /// </summary>
        [Test]
        public void Name_Property_ReturnsCorrectValue()
        {
            // Arrange
            const string expectedName = "Document Files";
            var filter = new ExtensionFilter(expectedName, "doc");

            // Act
            var actualName = filter.Name;

            // Assert
            Assert.AreEqual(expectedName, actualName, "Name property should return the value set in constructor");
        }

        /// <summary>
        /// Tests that Extensions property can be accessed correctly.
        /// </summary>
        [Test]
        public void Extensions_Property_ReturnsCorrectValue()
        {
            // Arrange
            var expectedExtensions = new[] { "doc", "docx", "pdf" };
            var filter = new ExtensionFilter("Documents", expectedExtensions);

            // Act
            var actualExtensions = filter.Extensions;

            // Assert
            Assert.IsNotNull(actualExtensions, "Extensions property should not be null");
            Assert.AreEqual(expectedExtensions.Length, actualExtensions.Length, "Extensions array length should match");
            
            for (int i = 0; i < expectedExtensions.Length; i++)
            {
                Assert.AreEqual(expectedExtensions[i], actualExtensions[i], $"Extension at index {i} should match");
            }
        }

        #endregion
    }
}

using System;
using System.IO;
using NUnit.Framework;
using USFB;

namespace USFB.Tests
{
    /// <summary>
    /// Essential unit tests for the StandaloneFileBrowser static class.
    /// Tests core functionality of the public API methods.
    /// </summary>
    [TestFixture]
    public class StandaloneFileBrowserTests
    {
        #region OpenFilePanel Tests

        /// <summary>
        /// Tests OpenFilePanel with valid parameters returns FileInfo array.
        /// </summary>
        [Test]
        public void OpenFilePanel_WithValidParameters_ReturnsFileInfoArray()
        {
            // Arrange
            const string title = "Open File";
            const string directory = @"C:\";
            var extensions = new[] { new ExtensionFilter("Text Files", "txt") };
            const bool multiselect = false;

            // Act
            var result = StandaloneFileBrowser.OpenFilePanel(title, directory, extensions, multiselect);

            // Assert
            Assert.IsNotNull(result, "OpenFilePanel should return a non-null result");
            Assert.IsInstanceOf<FileInfo[]>(result, "OpenFilePanel should return FileInfo array");
        }

        /// <summary>
        /// Tests OpenFilePanel with null parameters.
        /// </summary>
        [Test]
        public void OpenFilePanel_WithNullParameters_HandlesGracefully()
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = StandaloneFileBrowser.OpenFilePanel(null, null, (ExtensionFilter[])null, false);
                Assert.IsNotNull(result, "OpenFilePanel should return non-null result even with null parameters");
            });
        }

        /// <summary>
        /// Tests OpenFilePanel with multiselect enabled.
        /// </summary>
        [Test]
        public void OpenFilePanel_WithMultiselect_ReturnsFileInfoArray()
        {
            // Arrange
            const string title = "Select Multiple Files";
            var extensions = new[] { new ExtensionFilter("All Files", "*") };

            // Act
            var result = StandaloneFileBrowser.OpenFilePanel(title, "", extensions, true);

            // Assert
            Assert.IsNotNull(result, "OpenFilePanel with multiselect should return non-null result");
            Assert.IsInstanceOf<FileInfo[]>(result, "Result should be FileInfo array");
        }

        #endregion

        #region OpenFolderPanel Tests

        /// <summary>
        /// Tests OpenFolderPanel with valid parameters returns DirectoryInfo array.
        /// </summary>
        [Test]
        public void OpenFolderPanel_WithValidParameters_ReturnsDirectoryInfoArray()
        {
            // Arrange
            const string title = "Select Folder";
            const string directory = @"C:\";
            const bool multiselect = false;

            // Act
            var result = StandaloneFileBrowser.OpenFolderPanel(title, directory, multiselect);

            // Assert
            Assert.IsNotNull(result, "OpenFolderPanel should return a non-null result");
            Assert.IsInstanceOf<DirectoryInfo[]>(result, "OpenFolderPanel should return DirectoryInfo array");
        }

        /// <summary>
        /// Tests OpenFolderPanel with null parameters.
        /// </summary>
        [Test]
        public void OpenFolderPanel_WithNullParameters_HandlesGracefully()
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = StandaloneFileBrowser.OpenFolderPanel(null, null, false);
                Assert.IsNotNull(result, "OpenFolderPanel should return non-null result even with null parameters");
            });
        }

        /// <summary>
        /// Tests OpenFolderPanel with multiselect enabled.
        /// </summary>
        [Test]
        public void OpenFolderPanel_WithMultiselect_ReturnsDirectoryInfoArray()
        {
            // Arrange
            const string title = "Select Multiple Folders";

            // Act
            var result = StandaloneFileBrowser.OpenFolderPanel(title, "", true);

            // Assert
            Assert.IsNotNull(result, "OpenFolderPanel with multiselect should return non-null result");
            Assert.IsInstanceOf<DirectoryInfo[]>(result, "Result should be DirectoryInfo array");
        }

        #endregion

        #region SaveFilePanel Tests

        /// <summary>
        /// Tests SaveFilePanel with valid parameters returns FileInfo.
        /// </summary>
        [Test]
        public void SaveFilePanel_WithValidParameters_ReturnsFileInfo()
        {
            // Arrange
            const string title = "Save File";
            const string directory = @"C:\";
            const string defaultName = "document.txt";
            var extensions = new[] { new ExtensionFilter("Text Files", "txt") };

            // Act
            var result = StandaloneFileBrowser.SaveFilePanel(title, directory, defaultName, extensions);

            // Assert
            // Note: SaveFilePanel can return null when canceled, so we test the type when not null
            if (result != null)
            {
                Assert.IsInstanceOf<FileInfo>(result, "SaveFilePanel should return FileInfo when not canceled");
            }
        }

        /// <summary>
        /// Tests SaveFilePanel with null parameters.
        /// </summary>
        [Test]
        public void SaveFilePanel_WithNullParameters_HandlesGracefully()
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = StandaloneFileBrowser.SaveFilePanel(null, null, null, (ExtensionFilter[])null);
                // Note: SaveFilePanel can return null when canceled, so we don't assert IsNotNull here
            });
        }

        /// <summary>
        /// Tests SaveFilePanel with empty default name.
        /// </summary>
        [Test]
        public void SaveFilePanel_WithEmptyDefaultName_ReturnsFileInfo()
        {
            // Arrange
            const string title = "Save File";
            const string defaultName = "";
            var extensions = new[] { new ExtensionFilter("All Files", "*") };

            // Act
            var result = StandaloneFileBrowser.SaveFilePanel(title, "", defaultName, extensions);

            // Assert
            // Note: SaveFilePanel can return null when canceled, so we test the type when not null
            if (result != null)
            {
                Assert.IsInstanceOf<FileInfo>(result, "SaveFilePanel should return FileInfo when not canceled");
            }
        }

        #endregion
    }
}

const StandaloneFileBrowserWebGLPlugin = {
    // Open a file. This creates a hidden file input element and clicks it.
    // gameObjectNamePtr: Unique GameObject name. Required for calling back unity with SendMessage.
    // methodNamePtr: Callback method name on given GameObject.
    // filter: Filter files. Example filters:
    //     Match all image files: "image/*"
    //     Match all video files: "video/*"
    //     Match all audio files: "audio/*"
    //     Custom: ".plist, .xml, .yaml"
    // multiselect: Allows multiple file selection
    UploadFile: function (gameObjectNamePtr, methodNamePtr, filterPtr, multiselect) {
        let gameObjectName = UTF8ToString(gameObjectNamePtr);
        let methodName = UTF8ToString(methodNamePtr);
        let filter = UTF8ToString(filterPtr);

        // Delete the element if already exist
        let fileInput = document.getElementById(gameObjectName);
        if (fileInput && fileInput.parentNode === document.body) {
            document.body.removeChild(fileInput);
        }

        fileInput = document.createElement('input');
        fileInput.setAttribute('id', gameObjectName);
        fileInput.setAttribute('type', 'file');
        fileInput.setAttribute('style', 'display:none;');
        fileInput.setAttribute('style', 'visibility:hidden;');
        if (multiselect) {
            fileInput.setAttribute('multiple', '');
        }
        if (filter) {
            fileInput.setAttribute('accept', filter);
        }
        fileInput.onclick = function (event) {
            // File dialog opened
            this.value = null;
        };

        // Add cleanup for when user cancels file dialog
        fileInput.oncancel = function (event) {
            // User cancelled file dialog - clean up the element
            if (fileInput && fileInput.parentNode === document.body) {
                document.body.removeChild(fileInput);
            }
        };
        fileInput.onchange = function (event) {
            // multiselect works
            let urls = [];
            for (let i = 0; i < event.target.files.length; i++) {
                urls.push(URL.createObjectURL(event.target.files[i]));
            }
            // File selected
            SendMessage(gameObjectName, methodName, urls.join());

            // Remove after a file selected - check if the element is still a child before removing
            if (fileInput && fileInput.parentNode === document.body) {
                document.body.removeChild(fileInput);
            }
        }
        document.body.appendChild(fileInput);

        document.onmouseup = function () {
            fileInput.click();
            document.onmouseup = null;

            // Fallback cleanup after 30 seconds in case dialog events don't fire
            setTimeout(function () {
                if (fileInput && fileInput.parentNode === document.body) {
                    document.body.removeChild(fileInput);
                }
            }, 30000);
        }
    },

    // Save file
    // DownloadFile method does not open SaveFileDialog like standalone builds, its just allows user to download file
    // gameObjectNamePtr: Unique GameObject name. Required for calling back unity with SendMessage.
    // methodNamePtr: Callback method name on given GameObject.
    // filenamePtr: Filename with extension
    // byteArray: byte[]
    // byteArraySize: byte[].Length
    DownloadFile: function (gameObjectNamePtr, methodNamePtr, filenamePtr, byteArray, byteArraySize) {
        let gameObjectName = UTF8ToString(gameObjectNamePtr);
        let methodName = UTF8ToString(methodNamePtr);
        let filename = UTF8ToString(filenamePtr);

        let bytes = new Uint8Array(byteArraySize);
        for (let i = 0; i < byteArraySize; i++) {
            bytes[i] = HEAPU8[byteArray + i];
        }

        let downloader = window.document.createElement('a');
        downloader.setAttribute('id', gameObjectName);
        downloader.href = window.URL.createObjectURL(new Blob([bytes], {type: 'application/octet-stream'}));
        downloader.download = filename;
        document.body.appendChild(downloader);

        document.onmouseup = function () {
            downloader.click();
            // Check if the element is still a child before removing
            if (downloader && downloader.parentNode === document.body) {
                document.body.removeChild(downloader);
            }
            document.onmouseup = null;

            SendMessage(gameObjectName, methodName);
        }
    }
};

mergeInto(LibraryManager.library, StandaloneFileBrowserWebGLPlugin);
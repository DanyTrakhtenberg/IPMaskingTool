import { Injectable } from "@angular/core";
import { FileUploader, FileItem, FileUploaderOptions } from "ng2-file-upload";


export class FileUploaderCustom extends FileUploader {

setOptions(options: FileUploaderOptions){
  this.options = options;
}
  uploadAllFilesAtOnes(): void {
    var xhr = new XMLHttpRequest();
    var sendable = new FormData();
    var fakeitem: FileItem = null;
    this.onBuildItemForm(fakeitem, sendable);

    for (const item of this.queue) {
      item.isReady = true;
      item.isUploading = true;
      item.isUploaded = false;
      item.isSuccess = false;
      item.isCancel = false;
      item.progress = 0;

      item.isError = false;
      if (typeof item._file.size !== "number") {
        throw new TypeError("The file specified is no longer valid");
      }
      sendable.append("files", item._file, item.file.name);
    }

    if (this.options.additionalParameter !== undefined) {
      Object.keys(this.options.additionalParameter).forEach((key) => {
        sendable.append(key, this.options.additionalParameter[key]);
      });
    }

    xhr.onload = () => {
      var gist =
        (xhr.status >= 200 && xhr.status < 300) || xhr.status === 304
          ? "Success"
          : "Error";
      var method = "on" + gist + "Item";
      this[method](fakeitem, null, xhr.status, null);
    };
    xhr.onerror = () => {
      this.onErrorItem(fakeitem, null, xhr.status, null);
    };

    xhr.onabort = () => {
      this.onErrorItem(fakeitem, null, xhr.status, null);
    };

    xhr.open("POST", this.options.url, true);
    xhr.withCredentials = true;
    if (this.options.headers) {
      for (var _i = 0, _a = this.options.headers; _i < _a.length; _i++) {
        var header = _a[_i];
        xhr.setRequestHeader(header.name, header.value);
      }
    }
    if (this.authToken) {
      xhr.setRequestHeader(this.authTokenHeader, this.authToken);
    }
    xhr.send(sendable);
  }
}

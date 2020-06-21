import { Component } from "@angular/core";
import { FileUploader, FileLikeObject } from "ng2-file-upload";
import * as FileSaver from "file-saver";

// const URL = '/api/';
const URL = "IPMasking/post";

@Component({
  selector: "ng2-file-loader",
  templateUrl: "./ng2-file-loader.component.html",
})
export class Ng2FileLoaderComponent {
  uploader: FileUploader;
  hasBaseDropZoneOver: boolean;
  hasAnotherDropZoneOver: boolean;
  response: string;
  maxFileSize = 5 * 1024 * 1024;
  allowedMimeType = ["text/plain"];
  constructor() {
    this.uploader = new FileUploader({
      url: URL,
      allowedMimeType: this.allowedMimeType,
      maxFileSize: this.maxFileSize,
      headers: [
        { name: "Accept", value: "text/plain" },
        { name: "responseType", value: "blob" },
      ],
    });

    this.uploader.onBeforeUploadItem = (item: any) => {
      item._xhr.responseType = "blob";
    };
    this.hasBaseDropZoneOver = false;
    this.hasAnotherDropZoneOver = false;

    this.response = "";

    this.uploader.onSuccessItem = (
      item: any,
      response: any,
      status: any,
      headers: any
    ) => {
      var blob = new Blob([response], {
        type: "text/plain",
      });
      FileSaver.saveAs(blob, item.file.name);
    };
    this.uploader.onWhenAddingFileFailed = (item, filter, options) =>
      this.onWhenAddingFileFailed(item, filter, options);
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  public fileOverAnother(e: any): void {
    this.hasAnotherDropZoneOver = e;
  }
  onWhenAddingFileFailed(item: FileLikeObject, filter: any, options: any) {
    let errorMessage = "";
    switch (filter.name) {
      case "fileSize":
        errorMessage = `Maximum upload size exceeded (${item.size} of ${this.maxFileSize} allowed)`;
        break;
      case "mimeType":
        const allowedTypes = this.allowedMimeType.join();
        errorMessage = `Type "${item.type} is not allowed. Allowed types: "${allowedTypes}"`;
        break;
      default:
        errorMessage = `Unknown error (filter is ${filter.name})`;
    }
    alert(errorMessage);
    console.error(errorMessage);
  }
}

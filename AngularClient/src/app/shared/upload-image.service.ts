import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class UploadImageService {
    readonly BaseURI = 'http://localhost:7609/api'

  constructor(private http : HttpClient) { }

  postFile(caption: string, fileToUpload: File, userName: string) {
    const endpoint = this.BaseURI + '/Image/UploadImage/' + userName;
    const formData: FormData = new FormData();
    formData.append('Image', fileToUpload, fileToUpload.name);
    formData.append('ImageCaption', caption);
    return this.http
      .post(endpoint, formData);
  }

}
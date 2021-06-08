import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class UploadImageService {
    readonly BaseURI = 'http://localhost:7609/api'

  constructor(private http : HttpClient) { }

  postFile(fileToUpload: File, userName: string) {
    const endpoint = this.BaseURI + '/Image/UploadImage/' + userName;
    const formData: FormData = new FormData();
    formData.append('Image', fileToUpload, fileToUpload.name);
    return this.http
      .post(endpoint, formData);
  }

  downloadFile(userName: string) : Observable<HttpEvent<Blob>>{
    return this.http.request(new HttpRequest(
      'GET',
      `${this.BaseURI}/Image/GetImage/${userName}`,
      null,
      {
        reportProgress: true,
        responseType: 'blob'
      }));
  }
}
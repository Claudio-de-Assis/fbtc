import { Injectable } from '@angular/core';
import { AppSettings } from '../../../app.settings';

@Injectable()
export class FileUploadRoute {

    private url = 'FileUpload/';

    // [Route("GetAll")]
    sendFile(): string {
        return AppSettings.API_ENDPOINT + this.url + 'SendFile';
    }

    getFile(): string {
        return AppSettings.API_ENDPOINT + this.url + 'GetFile';
    }

    getImageFolder(): string {
        return AppSettings.API_ENDPOINT.substring(0, AppSettings.API_ENDPOINT.length - 4) + 'UploadedFiles/';
    }
}

import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { FileUploadService } from '../services/file-upload.service';
@NgModule({
  imports: [
    CommonModule,
    FormsModule
  ],
  declarations: [
    FileUploadComponent
  ],
  exports: [
    FileUploadComponent
  ],
  providers: [
    FileUploadService
  ]
})
export class FileUploadModule {
}
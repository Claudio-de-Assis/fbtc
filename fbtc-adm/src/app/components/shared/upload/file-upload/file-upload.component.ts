import { UserProfileService } from './../../services/user-profile.service';
import { Component, OnInit, Input, Output, EventEmitter, HostListener } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';

import { FileUploadService } from './../../services/file-upload.service';
import { EventoService } from './../../services/evento.service';
import { AssociadoService } from './../../services/associado.service';
import { ValueShareService } from './../../services/value-share.service';

import { AssociadoRoute } from '../../webApi-routes/associado.route';
import { FileUploadRoute } from './../../webapi-routes/file-upload.route';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit, OnDestroy {

  errors: Array<string> = [];
  dragAreaClass: string;
  @Input() targetId: number;
  @Input() projectId: string;
  @Input() sectionId: string;
  @Input() fileExt: string;
  @Input() maxFiles: number;
  @Input() maxSize: number;
  @Output() uploadStatus = new EventEmitter();

  subscription: Subscription;

  @Input() nomeFoto: string;
  @Input() srcFoto: string;

  nomefotoAnterior: string;

  history: string[] = [];

  constructor(
    private fileService: FileUploadService,
    private apiRoute: FileUploadRoute,
    private associadoRoute: AssociadoRoute,
    private valueShareService: ValueShareService,
    private associadoService: AssociadoService,
    private eventoService: EventoService,
    private userProfileService: UserProfileService
  ) {
    this.dragAreaClass = 'dragarea';
    this.fileExt = 'JPG, GIF, PNG';
    this.maxFiles = 1;
    this.maxSize = 2; // 2MB
  }

  getNomeImagemByAssociadoId(id: number): void {

    this.associadoService.getNomeImagemById(id)
        .subscribe(nomeFoto => this.nomeFoto = nomeFoto);
  }

  getNomeImagemByPessoaId(id: number): void {

    this.userProfileService.getNomeImagemByPessoaId(id)
        .subscribe(nomeFoto => this.nomeFoto = nomeFoto);
  }

  getNomeImagemByEventoId(id: number): void {

    this.eventoService.getNomeImagemById(id)
        .subscribe(nomeFoto => this.nomeFoto = nomeFoto);
  }

  saveFiles(files) {

    // this.nomefotoAnterior = this.nomeFoto;
    // console.log('anterior' + this.nomefotoAnterior);

    this.errors = []; // Clear error
    // Validate file size and allowed extensions
    if (files.length > 0 && (!this.isValidFiles(files))) {
        this.uploadStatus.emit(false);
        return;
    }
    if (files.length > 0) {
          const formData: FormData = new FormData();
          for (let j = 0; j < files.length; j++) {
              formData.append('file[]', files[j], files[j].name);
          }
          const parameters = {
              projectId: this.projectId,
              sectionId: this.sectionId
          };
          this.fileService.upload(formData, parameters)
              .subscribe(
              success => {
                this.uploadStatus.emit(true);
                this.nomeFoto = success.message.substring(8);
                this.srcFoto = this.apiRoute.getImageFolder();

                this.valueShareService.addValueString(this.nomeFoto);

                console.log(success);
              },
              error => {
                  this.uploadStatus.emit(true);
                  this.errors.push(error.ExceptionMessage);
              });
      }
  }

  onFileChange(event) {
    const files = event.target.files;
    this.saveFiles(files);
 }

 @HostListener('dragover', ['$event']) onDragOver(event) {
  this.dragAreaClass = 'droparea';
  event.preventDefault();
  }

  @HostListener('dragenter', ['$event']) onDragEnter(event) {
  this.dragAreaClass = 'droparea';
  event.preventDefault();
  }

  @HostListener('dragend', ['$event']) onDragEnd(event) {
    this.dragAreaClass = 'dragarea';
    event.preventDefault();
  }

  @HostListener('dragleave', ['$event']) onDragLeave(event) {
    this.dragAreaClass = 'dragarea';
    event.preventDefault();
  }

  @HostListener('drop', ['$event']) onDrop(event) {
    this.dragAreaClass = 'dragarea';
    event.preventDefault();
    event.stopPropagation();
    const files = event.dataTransfer.files;
    this.saveFiles(files);
  }

  private isValidFiles(files) {
    // Check Number of files
     if (files.length > this.maxFiles) {
         this.errors.push('Erro: At a time you can upload only ' + this.maxFiles + ' files');
         return;
     }
     this.isValidFileExtension(files);
     return this.errors.length === 0;
  }

  private isValidFileExtension(files) {
    // Make array of file extensions
    const extensions = (this.fileExt.split(','))
                    .map(function (x) { return x.toLocaleUpperCase().trim(); });
    for (let i = 0; i < files.length; i++) {
        // Get file extension
        const ext = files[i].name.toUpperCase().split('.').pop() || files[i].name;
        // Check the extension exists
        const exists = extensions.includes(ext);
        if (!exists) {
            this.errors.push('Erro (Extensão): ' + files[i].name);
        }
        // Check file size
        this.isValidFileSize(files[i]);
    }
  }

  private isValidFileSize(file) {
    const fileSizeinMB = file.size / (1024 * 1000);
    const size = Math.round(fileSizeinMB * 100) / 100; // convert upto 2 decimal place
    if (size > this.maxSize) {
      this.errors.push('Erro (Tamanho do arquivo):' + file.name + ': Excede o tamanho limite de ' + this.maxSize + 'MB ( ' + size + 'MB )');
    }
  }

  ngOnInit(): void {

    if (this.sectionId === 'AS') {

      this.nomeFoto = '_no-foto.png';
        // Associados:
      this.srcFoto = this.apiRoute.getImageFolder();

      if (this.targetId > 0) {
        this.getNomeImagemByAssociadoId(this.targetId);
      }
    } else if (this.sectionId === 'EV') {

      this.nomeFoto = '_no-foto-evento.jpg';
      // Eventos:
      this.srcFoto = this.apiRoute.getImageFolder();

      if (this.targetId > 0) {
        this.getNomeImagemByEventoId(this.targetId);
      }
    } else if (this.sectionId === 'UP') {

      this.nomeFoto = '_no-foto.png';
      // UserProfile:
      this.srcFoto = this.apiRoute.getImageFolder();

      if (this.targetId > 0) {
        this.getNomeImagemByPessoaId(this.targetId);
      }
    }
  }

  ngOnDestroy() {
    // prevent memory leak when component destroyed
    // this.subscription.unsubscribe();
  }
}

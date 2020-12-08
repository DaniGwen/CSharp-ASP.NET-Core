import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ImageFile } from '../../Models/imageFile';
import { Knive } from '../../Models/knive'
import { KnivesService } from '../../Services/knives.service';

@Component({
  selector: 'app-add-knive',
  templateUrl: './add-knive.component.html',
  styleUrls: ['./add-knive.component.scss']
})

export class AddKniveComponent {
  public isLoading: boolean = false;
  private kniveId: number;
  selectedFiles: FileList;
  imageFile = new ImageFile();
  private knive: Knive;
  public responceMessage: any;

  public addKniveForm = this.formBuilder.group({
    edgeWidth: ['', [Validators.required]],
    edgeThickness: ['', [Validators.required]],
    edgeLength: ['', [Validators.required]],
    totalLength: ['', [Validators.required]],
    edgeMade: ['', Validators.required],
    handleDescription: ['', Validators.required],
    kniveClass: ['', Validators.required],
    price: ['', [Validators.required]]
  });

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private kniveService: KnivesService) {
  }

  onSubmit() {
    this.isLoading = true;

    this.knive = this.addKniveForm.value;

    this.kniveService.add(this.knive).subscribe((kniveId: number) => {
      this.kniveId = kniveId;
      this.uploadFiles();
      this.responceMessage = `Knive with ID ${this.knive.kniveId} was added.`;
      this.isLoading = false;
    });
  }

  selectFiles(event) {

    const files = event.target.files;
    let isImage = true;

    for (let i = 0; i < files.length; i++) {
      if (files.item(i).type.match('image.*')) {
        continue;
      }
      else {
        isImage = false;
        alert('invalid format!');
        break;
      }
    }

    if (isImage) {
      this.selectedFiles = event.target.files;
    }
    else {
      this.selectedFiles = undefined;
      event.srcElement.percentage = null;
    }
  }

  uploadFiles() {
    for (let i = 0; i < this.selectedFiles.length; i++) {
      this.upload(i, this.selectedFiles[i]);
    }
  }

  upload(idx, file) {
    this.imageFile.lastModified = file.lastModified;
    this.imageFile.lastModifiedDate = file.lastModifiedDate;
    this.imageFile.name = file.name;
    this.imageFile.size = file.size;
    this.imageFile.type = file.type;

    const formData = new FormData();

    formData.append('file', <File>file, file.name);
    this.kniveService.uploadImage(formData, this.kniveId).subscribe(data => {
      this.responceMessage = data;
    });
  }
}

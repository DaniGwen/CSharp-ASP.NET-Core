import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { KnivesService } from '../../Services/knives.service';
import { Knive } from '../../Models/knive';
import { FormGroup, FormControl, NgForm } from '@angular/forms';
import { ImageFile } from '../../Models/imageFile';

@Component({
  selector: 'app-edit-knive',
  templateUrl: './edit-knive.component.html',
  styleUrls: ['./edit-knive.component.scss']
})
/** edit-knive component*/
export class EditKniveComponent {
  selectedFiles: FileList;
  status: any;
  kniveId: any;
  knive = new Knive();
  imageFile = new ImageFile();

  constructor(private route: ActivatedRoute,
    private kniveService: KnivesService) {
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.kniveId = params.get("id");
    });

    this.kniveService.getKniveById(this.kniveId).subscribe((data: any) => {
      this.knive = data
    });
  }

  onSubmit() {
    this.kniveService.updateKnive(this.knive)
      .subscribe((data: string) => {
        this.status = data;
      })
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
      this.status = data;
    });
  }
}

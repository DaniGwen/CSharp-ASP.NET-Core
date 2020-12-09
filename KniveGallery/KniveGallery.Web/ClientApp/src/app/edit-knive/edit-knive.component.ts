import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { KnivesService } from '../../Services/knives.service';
import { Knive } from '../../Models/knive';
import { ImageFile } from '../../Models/imageFile';
import { ToastService } from 'angular-toastify';

@Component({
  selector: 'app-edit-knive',
  templateUrl: './edit-knive.component.html',
  styleUrls: ['./edit-knive.component.scss']
})
/** edit-knive component*/
export class EditKniveComponent {
  selectedFiles: FileList;
  public status: any;
  kniveId: any;
  public knive = new Knive();
  imageFile = new ImageFile();
  public isLoading: boolean;

  constructor(private route: ActivatedRoute,
    private kniveService: KnivesService,
    private router: Router) {
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
    this.isLoading = true;
   
    this.kniveService.updateKnive(this.knive).subscribe((data: any) => {
        this.status = data;
      this.isLoading = false;
      });

    this.uploadFiles();
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

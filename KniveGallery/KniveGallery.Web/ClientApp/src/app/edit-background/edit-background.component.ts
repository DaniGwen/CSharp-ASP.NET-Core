import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BackgroundImage } from 'src/Models/backgroundImage';
import { ImageFile } from 'src/Models/imageFile';
import { ImageService } from 'src/Services/images.service';

@Component({
  selector: 'app-add-carousel-images',
  templateUrl: './edit-background.component.html',
  styleUrls: ['./edit-background.component.scss']
})
export class EditBackground {

  public isLoading: boolean = false;
  selectedFiles: FileList;
  imageFile = new ImageFile();
  public message: any;
  public allCarouselImages: BackgroundImage[];

  constructor(private imageService: ImageService,
    private router: Router) {
  }

  ngOnInit() {
    this.getAllImages();
  }

  onSubmit() {
    this.isLoading = true;
    this.uploadFiles();
  }

  deleteImage(imageId: number) {
    this.isLoading = true;
    this.imageService.deleteImageById(imageId).subscribe((message: any) => {
      if (message) {
        this.message = message;
      }
      this.isLoading = false;
    });
    //setInterval(() => { this.message = null }, 5000)
    this.reload("edit-background");
  }

  getAllImages() {
    this.isLoading = true;
    this.imageService.getBackgroundImages().subscribe((images: BackgroundImage[]) => {
      if (images) {
        this.allCarouselImages = images;
        this.isLoading = false;
      }
    });
  }

  selectImage(event) {
    const files = event.target.files;
    let isImage = true;

    for (let i = 0; i < files.length; i++) {
      if (files.item(i).type.match('image.*'))
        continue;
      else {
        isImage = false;
        alert('invalid format!');
        break;
      }
    }

    if (isImage)
      this.selectedFiles = event.target.files;
    else {
      this.selectedFiles = undefined;
      event.srcElement.percentage = null;
    }
  }

  selectVideo(event) {

  }

  uploadFiles() {
    if (this.selectedFiles != null) {
      for (let i = 0; i < this.selectedFiles.length; i++) {
        this.upload(i, this.selectedFiles[i]);
      }
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
    this.imageService.addBackgroundImage(formData).subscribe();
  }

  async reload(url: string): Promise<boolean> {
    await this.router.navigateByUrl('.', { skipLocationChange: true });
    return this.router.navigateByUrl(url);
  }
}

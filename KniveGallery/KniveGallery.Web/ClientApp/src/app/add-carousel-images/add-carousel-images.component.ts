import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CarouselImage } from 'src/Models/carouselImage';
import { ImageFile } from 'src/Models/imageFile';
import { ImageService } from 'src/Services/images.service';

@Component({
    selector: 'app-add-carousel-images',
    templateUrl: './add-carousel-images.component.html',
    styleUrls: ['./add-carousel-images.component.scss']
})
/** add-carousel-images component*/
export class AddCarouselImagesComponent {

    public isLoading: boolean = false;
    selectedFiles: FileList;
    imageFile = new ImageFile();
    public message: any;
    public allCarouselImages: CarouselImage[];

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
        this.reload("add-carousel-images");
    }

    getAllImages() {
        this.isLoading = true;
        this.imageService.getCarouselImages().subscribe((images: CarouselImage[]) => {
            if (images) {
                this.allCarouselImages = images;
                this.isLoading = false;
            }
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
        this.imageService.addCarouselImage(formData).subscribe();
    }

    async reload(url: string): Promise<boolean> {
        await this.router.navigateByUrl('.', { skipLocationChange: true });
        return this.router.navigateByUrl(url);
      }
}

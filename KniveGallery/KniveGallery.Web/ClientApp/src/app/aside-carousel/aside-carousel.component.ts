import { Component } from '@angular/core';
import { CarouselImage } from '../../Models/carouselImage';
import { ImageService } from '../../Services/images.service';

@Component({
  selector: 'app-aside-carousel',
  templateUrl: './aside-carousel.component.html',
  styleUrls: ['./aside-carousel.component.scss']
})

export class AsideCarouselComponent {
  public images: CarouselImage[];
  public activeImage: CarouselImage;

  constructor(private imageService: ImageService) {
  }

  ngOnInit() {
    this.imageService.getCarouselImages().subscribe((carouselImages: CarouselImage[]) => {
      if (carouselImages) {
        this.activeImage = carouselImages.reverse().pop();
        this.images = carouselImages.reverse();
      }
    });
  }
}

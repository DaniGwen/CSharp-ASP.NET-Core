import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../environments/environment";
import { CarouselImage } from "../Models/carouselImage";

@Injectable({
    providedIn: 'root'
})
export class ImageService {
    private headers = new HttpHeaders();
    Url = environment.apiUrl + '/api/images';

    constructor(private http: HttpClient) {
    }

    public getCarouselImages() {
        return this.http.get<CarouselImage[]>(this.Url);
    }

    public addCarouselImage(form: FormData) {
        return this.http.post(`${this.Url}/AddCarouselImage`, form, { headers: this.headers });
    }

    public deleteImageById(imageId: number) {
        return this.http.post<string>(`${this.Url}/deleteImage/${imageId}`, { headers: this.headers });
    }
}

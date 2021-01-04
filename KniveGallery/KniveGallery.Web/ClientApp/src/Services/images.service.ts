import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
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

    public getCarouselImages(): Observable<CarouselImage[]> {
        return this.http.get<CarouselImage[]>(this.Url, { headers: this.headers });
    }

    public addCarouselImage(form: FormData) {
        return this.http.post(`${this.Url}/AddCarouselImage`, form, { headers: this.headers });
    }

    public deleteImageById(imageId: number) {
        return this.http.post<string>(`${this.Url}/deleteImage/${imageId}`, { headers: this.headers });
    }
}

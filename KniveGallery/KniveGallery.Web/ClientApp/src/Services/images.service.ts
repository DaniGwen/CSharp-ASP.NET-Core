import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { BackgroundImage } from "../Models/backgroundImage";

@Injectable({
    providedIn: 'root'
})
export class ImageService {
    private headers = new HttpHeaders();
    Url = environment.apiUrl + '/api/images';

    constructor(private http: HttpClient) {
    }

    public getBackgroundImages(): Observable<BackgroundImage[]> {
      return this.http.get<BackgroundImage[]>(this.Url, { headers: this.headers });
    }

    public addBackgroundImage(form: FormData) {
        return this.http.post(`${this.Url}/AddBackgroundImage`, form, { headers: this.headers });
    }

    public deleteImageById(imageId: number): Observable<string> {
        return this.http.post<string>(`${this.Url}/deleteImage/${imageId}`, { headers: this.headers });
    }
}

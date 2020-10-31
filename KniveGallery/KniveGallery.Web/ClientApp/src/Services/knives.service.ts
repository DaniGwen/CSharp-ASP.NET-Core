import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class KnivesService {

  private headers = new HttpHeaders();
  Url = 'https://localhost:44379/api/knives';

  constructor(private http: HttpClient) { }

  getKnivesByClass(kniveClass: string) {
    return this.http.get(`${this.Url}/KniveClass/${kniveClass}`);
  }

  getAllknives() {
    return this.http.get(this.Url);
  }

  getKniveById(kniveId: number) {
    return this.http.get(`${this.Url}/${kniveId}`);
  }

  public get() {
    return this.http.get(this.Url);
  }

  public add(knive) {
    return this.http.post(this.Url, knive, { headers: this.headers });
  }

  public removeKnive(kniveId) {
    return this.http.delete(`${this.Url}/${kniveId}`, { headers: this.headers });
  }

  public updateKnive(knive) {
    return this.http.put(`${this.Url}/${knive.kniveId}`, knive, { headers: this.headers });
  }

  public uploadImage(form: FormData, kniveId: number) {
    return this.http.post(`${this.Url}/AddImage/${kniveId}`, form, { headers: this.headers });
  }

  public getKniveImages(kniveId: number) {
    return this.http.get(`${this.Url}/AllKniveImages/${kniveId}`, { headers: this.headers });
  }

  public getAdminDetails() {
    return this.http.get(`${this.Url}/AdminDetails`, { headers: this.headers });
  }
}

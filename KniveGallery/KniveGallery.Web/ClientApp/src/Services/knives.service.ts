import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment'
import { Observable } from 'rxjs';
import { Knive } from '../Models/knive';

@Injectable({
  providedIn: 'root'
})
export class KnivesService {

  private headers = new HttpHeaders();
  Url = environment.apiUrl + '/api/knives';

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

  public add(knive: Knive): Observable<number> {
    return this.http.post<number>(`${this.Url}/AddKnive`, knive, { headers: this.headers });
  }

  public removeKnive(kniveId) {
    return this.http.delete(`${this.Url}/${kniveId}`, { headers: this.headers });
  }

  public updateKnive(knive: Knive) {
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

  public updateKniveLikes(isLiked: boolean, kniveId: number): Observable<Knive> {
    return this.http.post<Knive>(`${this.Url}/UpdateKniveLikes/${kniveId}/${isLiked}`, { headers: this.headers })
  }
}

import { Injectable, Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Knive } from '../Models/knive';
import { from, Observable, throwError } from 'rxjs';

import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class KnivesService {

  private headers: HttpHeaders;
  Url = 'https://localhost:44379/api/knives';

  constructor(private http: HttpClient) { }

  getAllknives() {
    return this.http.get(this.Url);
  }

  public get() {
    return this.http.get(this.Url);
  }

  public add(knive) {
    return this.http.post(this.Url, knive, { headers: this.headers });
  }

  public remove(knive) {
    return this.http.delete(this.Url + '/' + knive.id, { headers: this.headers });
  }

  public update(knive) {
    return this.http.put(this.Url + '/' + knive.id, { headers: this.headers });
  }
}

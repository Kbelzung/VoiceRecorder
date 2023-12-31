import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MemoService {
  private apiUrl = 'https://localhost:7241/api/Memo';

  constructor(private http: HttpClient) { }

  getMemos(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}`);
  }
}

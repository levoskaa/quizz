import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Globals } from '@globals';
import { Observable } from 'rxjs';
import { objectToHttpParams } from '../utils/object-to-http-params';

@Injectable({
  providedIn: 'root',
})
export class AppHttpClient {
  private readonly apiBaseUrl = `${Globals.apiRoot}/api`;

  constructor(private readonly httpClient: HttpClient) {}

  post<TResponse, TBody>(url: string, body: TBody): Observable<TResponse> {
    return this.httpClient.post<TResponse>(`${this.apiBaseUrl}${url}`, body);
  }

  get<TResponse, TParams>(url: string, params?: TParams): Observable<TResponse> {
    return this.httpClient.get<TResponse>(`${this.apiBaseUrl}${url}`, {
      params: objectToHttpParams(params),
    });
  }

  put<TResponse, TBody>(url: string, body: TBody): Observable<TResponse> {
    return this.httpClient.put<TResponse>(`${this.apiBaseUrl}${url}`, body);
  }

  delete<TResponse>(url: string): Observable<TResponse> {
    return this.httpClient.delete<TResponse>(`${this.apiBaseUrl}${url}`);
  }
}

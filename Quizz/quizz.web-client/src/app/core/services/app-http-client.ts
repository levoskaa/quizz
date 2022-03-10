import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppHttpClient {
  constructor(private readonly httpClient: HttpClient) {}

  post<TResponse, TBody>(url: string, body: TBody): Observable<TResponse> {
    return this.httpClient.post<TResponse>(url, { body });
  }

  get<TResponse, TParams>(url: string, params?: TParams): Observable<TResponse> {
    const httpParams = new HttpParams({ fromObject: { ...params } });
    return this.httpClient.get<TResponse>(url, { params: httpParams });
  }

  put<TResponse, TBody>(url: string, body: TBody): Observable<TResponse> {
    return this.httpClient.put<TResponse>(url, { body });
  }

  delete<TResponse>(url: string): Observable<TResponse> {
    return this.httpClient.delete<TResponse>(url);
  }
}

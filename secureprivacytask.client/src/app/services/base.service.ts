import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BinaryResult } from '../models/binary-result.model'
import { Observable, catchError, of, tap } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(private http: HttpClient) {

  }

  verifyBinaryString(binaryValue: string): Observable<BinaryResult> {
    
    console.log('verifyBinaryString');

    var userObs = this.http.get<BinaryResult>('/base/VerifyBinaryString?binaryValue=' + binaryValue).pipe(
      catchError(error => {
        console.error(error);
        return of();
      })
    );

    return userObs;
  }

}

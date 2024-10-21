import { Injectable } from '@angular/core';
import { Observable, catchError, of, tap } from 'rxjs';
import { User, UserFilter } from '../models/user.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  users: User[] = []; 

  constructor(private http: HttpClient) {

  }

  getUsers(filter: UserFilter): Observable<User[]> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    console.log('getUsers');

    console.log('filter');
    console.log(filter);

    var userObs = this.http.post<User[]>('/user/GetUserList', filter, { headers }).pipe(
      tap(users => {
        console.log('Utenti letti:', users);
      }),
      catchError(error => {
        console.error(error);
        return of([]); 
      })
    );

    return userObs;
  }


  updateUser(user: User): Observable<User> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    const url = '/user/UpdateUser?Id=' + user.id;

    return this.http.put<User>(url, user, { headers });
    return of(user); 
  }

 
  createUser(user: User): Observable<User> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    const url = '/user/CreateUser';

    return this.http.post<User>(url, user, { headers });
    return of(user); 
  }


  getUserById(userId: string): Observable<User> {
    return this.http.get<User>('/user/GetUserById?id=' + userId).pipe(
      catchError(error => {
        console.error(error);
        return of(); 
      })
    );
  }


  deleteUser(userId: number): Observable<boolean> {
    console.log('deleteUtente from API: ' + userId);

    const deleteOk = this.http.delete<boolean>('/user/DeleteUser?id=' + userId).pipe(
      catchError(error => {
        console.error(error);
        return of(false); 
      })
    );

    console.log(deleteOk);

    return deleteOk;
  }
}

import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { UserService } from '../../services/user.service';
import { loadUsers, loadUsersSuccess, loadUsersFailure } from '../actions/user.actions';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable()
export class UserEffects {
  loadUsers$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadUsers), 
      mergeMap((action) =>
        this.userService.getUsers(action.filter).pipe( 
          map(users => loadUsersSuccess({ users })), 
          catchError(error => of(loadUsersFailure({ error }))) 
        )
      )
    )
  );

  constructor(private actions$: Actions, private userService: UserService) { }
}

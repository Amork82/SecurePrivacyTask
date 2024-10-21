import { createAction, props } from '@ngrx/store';
import { User, UserFilter } from '../../models/user.model';


export const loadUsers = createAction(
  '[User Page] Load Users',
  props<{ filter: UserFilter }>() 
);

export const loadUsersSuccess = createAction('[User API] Load Users Success', props<{ users: User[] }>());
export const loadUsersFailure = createAction('[User API] Load Users Failure', props<{ error: string }>());

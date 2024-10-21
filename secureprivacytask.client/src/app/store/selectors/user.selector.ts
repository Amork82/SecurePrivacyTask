import { createSelector, createFeatureSelector } from '@ngrx/store';
import { UserState } from '../states/user.state';

export const userStateSelected = createFeatureSelector<UserState>('user');

export const selectUsers = createSelector(
  userStateSelected,
  (state: UserState) => state.users
);

export const selectUsersSelected = createSelector(
  userStateSelected,
  (state: UserState) => state.userSelected
);

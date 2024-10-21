import { User } from '../../models/user.model';

export interface UserState {
  users: User[];
  userSelected: User | null;
  loading: boolean;
  error: string | null;
}

export const initialState: UserState = {
  users: [],
  userSelected: null,
  loading: false,
  error: null
};

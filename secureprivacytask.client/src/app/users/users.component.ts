import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { User, UserFilter } from '../models/user.model';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { loadUsers } from '../store/actions/user.actions';
import { selectUsers } from '../store/selectors/user.selector';
import { Observable, filter, map, take } from 'rxjs';
import { UserState } from '../store/states/user.state';
import { EffectsModule } from '@ngrx/effects';
import { UserEffects } from '../store/effects/user.effects'; // Importa il file effects
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog/confirm-dialog.component';
import { UserService } from '../services/user.service';
import { MatSort, Sort, MatSortModule, MatSortHeader } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';





@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],

})
export class UsersComponent implements OnInit, AfterViewInit {
  @ViewChild(MatSort) sort: MatSort | undefined; //sort object for load users API
  users$: Observable<User[]> = this.store.select(selectUsers).pipe(
    map(utenti => utenti || []) //Empty array for no selection
  );

  users: User[] = [];
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'dateOfBirth', 'country', 'city', 'isEnabled', 'actions'];
  currentSort: { column: string, direction: 'asc' | 'desc' | null } = { column: '', direction: null };
  filterForm: FormGroup;

  ngAfterViewInit() {
    //this.utenti.sort = this.sort; // Collega MatSort a dataSource
  }

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private store: Store<UserState>,
    private userService: UserService,
    public dialog: MatDialog) {
    this.filterForm = this.fb.group({
      firstName: [''],
      lastName: [''],
      dateOfBirth: undefined,
      onlyEnabled: [false]
    });
  }

  ngOnInit() {
    // Initialize users list
    this.search();

  }

  setSort(column: string): void {
    if (this.currentSort.column === column) {
      // Switch order column selected
      this.currentSort.direction = this.currentSort.direction === 'asc' ? 'desc' : 'asc';
    } else {
      // Else, select new order column
      this.currentSort.column = column;
      this.currentSort.direction = 'asc'; // Set ascending as default order column
    }
    this.search(); // load user list with new order 
  }


  // Load users from API
  search() {
    console.log('search function');


    const userFilter: UserFilter = {
      ...this.filterForm.getRawValue(),
      orderField: this.currentSort.column,
      orderDirection: this.currentSort.direction
    }

    console.log('new user filter');
    console.log(userFilter);

    // API call with all criteria
    this.store.dispatch(loadUsers({ filter: userFilter }));
  }




  // new user dialog
  createUser() {
    console.log('Create new user');
    this.router.navigate([`/user-details`]);
  }

  // update existing user
  updateUser(id: number) {
    console.log('Update user with Id: ', id);
    this.router.navigate([`/user-details`, id]);
  }




  // Show dialog confirm for deleting user
  deleteUser(id: number) {
    console.log('Deleting user with Id:', id);
    this.openDeleteDialog(id);
  }

  // Open popup dialog confirm
  //TODO: insert message parameter
  openDeleteDialog(userId: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '300px',
      data: { userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.userService.deleteUser(userId).subscribe(
          deleteOK => {
            console.log('delete OK: ' + deleteOK);
            if (deleteOK) {
              this.search(); // re-load new user list without deleted user
            } else {
              console.error('Failed to delete the user');
            }
          },
          error => {
            console.error('Error during deletion:', error);
          }
        );
      }
    });
  }



}

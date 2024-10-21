import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { HomeComponent } from './home/home.component';
import { BinaryCheckComponent } from './binary-check/binary-check.component';

const routes: Routes = [
  { path: 'users', component: UsersComponent },
  { path: 'user-details/:id', component: UserDetailsComponent },
  { path: 'user-details', component: UserDetailsComponent },
  { path: 'home', component: HomeComponent },
  { path: 'verify', component: BinaryCheckComponent },
  { path: '', redirectTo: '/users', pathMatch: 'full' }, 
  { path: '**', redirectTo: '/users' } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

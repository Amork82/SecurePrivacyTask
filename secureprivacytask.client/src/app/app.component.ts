import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './models/user.model';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public usersApiList: User[] = [];

  selectedMenuTitle: string = 'Home';

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
  }





  title = 'Secure Privacy test - Marco Olivieri';
}

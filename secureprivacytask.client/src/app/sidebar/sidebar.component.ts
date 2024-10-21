import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {

  @Output() menuSelected = new EventEmitter<string>();

  constructor(private router: Router) { }

  onMenuClick(menu: string) {
    switch (menu) {
      case 'home':
        this.router.navigate(['/home']);
        break;
      case 'users':
        this.router.navigate(['/users']);
        break;
      case 'verify':
        this.router.navigate(['/verify']);
        break;
    }

  }
}

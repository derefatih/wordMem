import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@app/_services';
import { Router } from '@angular/router';
import { User } from '@app/_models';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  currentUser: User;
  constructor( private router: Router,
    private authenticationService: AuthenticationService,
    ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
   }

  ngOnInit() {
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
}

}

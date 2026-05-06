import { Component, OnInit, inject, signal } from '@angular/core';

import { AccountService } from '../core/services/account-service';
import { Home } from "../features/home/home";
import { HttpClient } from '@angular/common/http';
import { Nav } from '../layout/nav/nav';
import { User } from '../types/user';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [Nav, Home],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  private http = inject(HttpClient);
  protected readonly title = signal('Welcome to Dating App');
  protected members = signal<User[]>([]);


  async ngOnInit() {
    this.members.set(await this.getMembers());
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if(!userString)
      return;

    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  async getMembers(){
    try {
      return lastValueFrom(this.http.get<User[]>('https://localhost:5001/api/members'));
    } catch (error) {
      console.log('Error while fetching members');
      throw error;
    }
  }
}



import { Component, OnInit, inject, signal } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { errorContext } from 'rxjs/internal/util/errorContext';
import { lastValueFrom } from 'rxjs';

// import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [MatCardModule, MatButtonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected readonly title = signal('Welcome to Dating App');

  protected members = signal<any>([]);

  async ngOnInit() {
    this.members.set(await this.getMembers());
  }

  async getMembers(){
    try {
      return lastValueFrom(this.http.get('https://localhost:5001/api/members'));
    } catch (error) {
      console.log('Error while fetching members');
      throw error;
    }
  }
}



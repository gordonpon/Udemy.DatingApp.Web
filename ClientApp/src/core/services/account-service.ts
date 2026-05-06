import { Injectable, inject, signal } from '@angular/core';
import { LoignCreds, RegisterCreds, User } from '../../types/user';

import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  // 注入 HttpClient 以發送 HTTP 請求。
  private http = inject(HttpClient)

  currentUser = signal<User | null>(null);

  // 後端 API 的基底網址。
  baseUrl = 'https://localhost:5001/api/';

  register(creds: RegisterCreds) {
    return this.http.post<User>(this.baseUrl + 'account/register', creds).pipe(
      tap(user => {
        if(user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  // 呼叫登入 API，將帳號密碼資料送到後端驗證。
  login(creds: LoignCreds) {
    return this.http.post<User>(this.baseUrl + 'account/login', creds).pipe(
      tap(user => {
        if(user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user)
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}



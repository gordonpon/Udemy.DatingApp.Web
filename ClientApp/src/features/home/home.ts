import { Component, Input, signal } from '@angular/core';

import { Register } from "../account/register/register";
import { User } from '../../types/user';

@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  // 由父元件傳入的會員清單，標記為必填 Input。
  //@Input({required: true}) membersFromApp: User[] = [];

  // 控制是否顯示註冊表單的 Signal 狀態，預設為隱藏。
  protected registerMode = signal(false);

  // 將 registerMode 設為 true，切換顯示註冊表單。
  showRegister(value: boolean) {
    this.registerMode.set(value);
  }



}


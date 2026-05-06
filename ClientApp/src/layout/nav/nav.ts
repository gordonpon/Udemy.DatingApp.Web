import { Component, inject, signal } from '@angular/core';

import { AccountService } from '../../core/services/account-service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})

export class Nav {
  // 注入帳號服務，集中處理登入/註冊等 API 呼叫。
  protected accountService = inject(AccountService)

  // 與表單欄位綁定的登入資料物件，送出時作為請求內容。
  protected creds: any = {}

  // 送出登入請求，並分別處理成功與失敗結果。
  login() {
    this.accountService.login(this.creds).subscribe({
      // 登入成功：目前先輸出回傳資料，方便開發時檢查。
      next: result => {
        console.log(result);
        this.creds = {};
      },
      // 登入失敗：顯示後端回傳的錯誤訊息。
      error: error => alert(error.message)
    });
  }

  logout() {
    this.accountService.logout();
  }
}

import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';

import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

// 應用程式層級設定：集中註冊全域 provider。
export const appConfig: ApplicationConfig = {
  providers: [
    // 攔截並處理瀏覽器端的全域錯誤（例如未捕捉例外）。
    provideBrowserGlobalErrorListeners(),

    // 註冊路由設定，讓應用可依 URL 切換畫面。
    provideRouter(routes),

    // 啟用 HttpClient，供服務層發送 HTTP 請求。
    provideHttpClient()
  ]
};
/*
app.config.ts
它是 Angular（standalone 架構）應用的「全域設定入口」之一。
主要用來集中管理啟動時的 provider 註冊（路由、HTTP、全域錯誤處理、攔截器等）。
通常會在 main.ts 透過 bootstrapApplication(..., appConfig) 載入，使整個 App 共享同一套基礎設定。

*/

import { FileUploadModule } from 'ng2-file-upload';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { Ng2FileLoaderComponent } from './ng2-file-loader/ng2-file-loader.component';
import { IpMaskingComponent } from './ip-masking/ip-masking.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    Ng2FileLoaderComponent,
    IpMaskingComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    FileUploadModule,
    RouterModule.forRoot([
      { path: '', component: IpMaskingComponent, pathMatch: 'full' },
      { path: 'upload', component: IpMaskingComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenuComponent } from './navigation/menu/menu.component';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { cart } from 'ngx-bootstrap-icons';
import { NgxBootstrapIconsModule } from 'ngx-bootstrap-icons';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxSpinnerConfig } from 'ngx-spinner/lib/config';


const icons = {
  cart
};

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CollapseModule,
    NgxBootstrapIconsModule.pick(icons),
    NgxSpinnerModule.forRoot({
      type: "pacman"
    } as NgxSpinnerConfig)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ClientsComponent } from './clients/clients.component';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ClientChatsComponent } from './client-chats/client-chats.component';
import { DialogModule } from 'primeng/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ToastModule } from 'primeng/toast';
import { LoginComponent } from './login/login.component';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { MessageService } from 'primeng/api';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ClientsComponent,
    ClientChatsComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'clients', component: ClientsComponent },
      { path: 'clients/:id/assignedChats', component: ClientChatsComponent },
      { path: 'login', component: LoginComponent }
    ]),
    TableModule,
    ButtonModule,
    DialogModule,
    BrowserAnimationsModule,
    AutoCompleteModule,
    ToastModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule
  ],
  providers: [
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

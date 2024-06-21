import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ClientsComponent,
    ClientChatsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ClientsComponent, pathMatch: 'full' },
      { path: 'clients', component: ClientsComponent },
      { path: 'clients/:id/assignedChats', component: ClientChatsComponent },
    ]),
    TableModule,
    ButtonModule,
    DialogModule,
    BrowserAnimationsModule,
    AutoCompleteModule,
    ToastModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

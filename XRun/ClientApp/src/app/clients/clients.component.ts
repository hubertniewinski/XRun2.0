import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss']
})
export class ClientsComponent implements OnInit {
  public clients: ClientDashboardDto[] = [];

  constructor(private readonly http: HttpClient,
    @Inject('BASE_URL') private readonly baseUrl: string,
    private readonly router: Router,
    public readonly authService: AuthService) { }

  ngOnInit(): void {
    if(!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }
    setTimeout(() => {
      this.http.get<ClientDashboardDto[]>(this.baseUrl + 'api/clients?token=' + this.authService.getToken()).subscribe(result => {
        this.clients = result;
      }, error => console.error(error));
    }, 100);
  }

  navigateToClient(clientId: string) {
    this.router.navigate(["clients", clientId, "assignedChats"]);
  }
}

interface ClientDashboardDto {
  id: string;
  fullName: string;
  chatsCount: number;
}

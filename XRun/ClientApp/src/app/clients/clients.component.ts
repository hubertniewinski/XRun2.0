import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent {
  public clients: ClientDashboardDto[] = [];

  constructor(private readonly http: HttpClient,
    @Inject('BASE_URL') private readonly baseUrl: string, 
    private readonly router: Router) {
    http.get<ClientDashboardDto[]>(baseUrl + 'api/clients').subscribe(result => {
      this.clients = result;
    }, error => console.error(error));
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

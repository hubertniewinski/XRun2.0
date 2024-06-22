import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css']
})
export class ChatsComponent implements OnInit {
  constructor(private readonly http: HttpClient,
    @Inject('BASE_URL') private readonly baseUrl: string,
    private readonly router: Router,
    public readonly authService: AuthService) { }

  chatAssignments: ChatAssignment[] = [];
    
  ngOnInit(): void {
    if(!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }

    if(!this.authService.isAdmin()) {
      return;
    }

    this.http.get<ChatAssignment[]>(this.baseUrl + 'api/aichats/chatAssignments?token=' + this.authService.getToken()).subscribe(result => {
      this.chatAssignments = result;
    }, error => console.error(error));
  }
}

interface ChatAssignment {
  type: string;
  clients: string[];
}
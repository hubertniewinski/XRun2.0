import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-client-chats',
  templateUrl: './client-chats.component.html',
  styleUrls: ['./client-chats.component.scss']
})
export class ClientChatsComponent implements OnInit {
  constructor(
    private readonly http: HttpClient,
    @Inject('BASE_URL') private readonly baseUrl: string,
    private readonly route: ActivatedRoute,
    private readonly messageService: MessageService,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  id: string | null = null;

  assignedChats: string[] = [];
  
  availableChats: any[] | undefined;
  filteredChats: any[] | undefined;
  selectedChat: any | undefined;
  visible: boolean = false;

  hasViewPermission: boolean = false;
  hasAssignChatPermission: boolean = false;

  ngOnInit(): void {
    if(!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }
    this.route.paramMap.subscribe((params) => {
      this.id = params.get('id');
      this.hasViewPermission = this.authService.isAdmin() || this.authService.getId() == this.id;
      this.hasAssignChatPermission = this.authService.isAdmin();

      this.loadDetails();
    });
  }

  loadDetails() {
    this.http
    .get<string[]>(this.baseUrl + 'api/clients/' + this.id + '/assignedChats?token=' + this.authService.getToken())
    .subscribe(
      (result) => {
        this.assignedChats = result;
      },
      (error) => console.error(error)
    );
  }

  showDialog() {
    this.selectedChat = undefined;
    this.visible = true;
    this.error = undefined;

    this.http
    .get<string[]>(this.baseUrl + 'api/aichats/availableChats?token=' + this.authService.getToken())
    .subscribe(
      (result) => {
        this.availableChats = result.map(x => ({
          label: x,
          value: x
        }));
      },
      (error) => {
        this.messageService.add({severity:'error', summary:'Error', detail:error.error});
      }
    );
  }

  filterChat(event: AutoCompleteCompleteEvent) {
    let filtered: any[] = [];
    let query = event.query;

    for (let i = 0; i < (this.availableChats as any[]).length; i++) {
      let chat = (this.availableChats as any[])[i];
      if (chat.value.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(chat);
      }
    }

    this.filteredChats = filtered;
  }

  error: string | undefined;

  assignChat() {
    this.error = undefined;
    const chatType = this.selectedChat.value;
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this.http
    .post<string[]>(this.baseUrl + 'api/clients/assignChat/' + this.id, 
      {
        Token: this.authService.getToken(),
        ChatType: chatType
      }, { headers })
    .subscribe(
      (result) => {
        const detail = chatType + ' successfully assigned';
        this.messageService.add({severity:'success', summary:'Success', detail:detail, life: 1000000});
        this.visible = false;
        this.loadDetails();
      },
      (error) => {
        this.error = error.error;
      }
    );
  }
}

interface AutoCompleteCompleteEvent {
  originalEvent: Event;
  query: string;
}
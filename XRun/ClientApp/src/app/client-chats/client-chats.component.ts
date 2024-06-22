import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-client-chats',
  templateUrl: './client-chats.component.html',
  styleUrls: ['./client-chats.component.scss'],
  providers: [MessageService]
})
export class ClientChatsComponent implements OnInit {
  constructor(
    private readonly http: HttpClient,
    @Inject('BASE_URL') private readonly baseUrl: string,
    private readonly route: ActivatedRoute,
    private readonly messageService: MessageService
  ) {}

  id: string | null = null;

  assignedChats: string[] = [];
  
  availableChats: any[] | undefined;
  filteredChats: any[] | undefined;
  selectedChat: any | undefined;
  visible: boolean = false;

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.id = params.get('id');
      this.loadDetails();
    });
  }

  loadDetails() {
    this.http
    .get<string[]>(this.baseUrl + 'api/clients/' + this.id + '/assignedChats')
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
    .get<string[]>(this.baseUrl + 'api/aichats/availableChats')
    .subscribe(
      (result) => {
        this.availableChats = result.map(x => ({
          label: x,
          value: x
        }));
      },
      (error) => console.error(error)
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
      JSON.stringify(chatType), { headers })
    .subscribe(
      (result) => {
        this.messageService.add({severity:'success', summary:'Success', detail:'Chat assigned successfully', life: 1000000});
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
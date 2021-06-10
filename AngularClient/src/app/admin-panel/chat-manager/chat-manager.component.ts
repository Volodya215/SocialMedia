import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ChatService } from 'src/app/shared/chat.service';

@Component({
  selector: 'app-chat-manager',
  templateUrl: './chat-manager.component.html',
  styles: [
  ]
})
export class ChatManagerComponent implements OnInit {
  allMessages: any[] = [];

  constructor(private chatService: ChatService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getAllMessages();
  }

  getAllMessages() {
    this.chatService.getAllAdminMessages().subscribe(
      (result: any) => {
  		  this.allMessages = result;
      },
      err => {
        console.log(err);
      },
    );
  }

  deleteMessage(id: any) {
    this.chatService.deleteMessage(id).subscribe(
      (result: any) => {
  		  this.toastr.success('Message deleted!', 'Message successfully deleted');
        this.getAllMessages();
      },
      err => {
        this.toastr.error('Error to delete!', 'Something happened on the server');
        console.log(err);
      }
    );
  }

}

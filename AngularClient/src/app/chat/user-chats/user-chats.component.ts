import { HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ChatService, Message } from 'src/app/shared/chat.service';
import { UploadImageService } from 'src/app/shared/upload-image.service';

@Component({
  selector: 'app-user-chats',
  templateUrl: './user-chats.component.html',
  styleUrls: ['./user-chats.component.css']
})
export class UserChatsComponent implements OnInit {
  public message : Message;
  chats: any[]= [];
  messages: any[] = [];
  currentChatID: number = 0;
  public currentChatName: string = "";
  currentUserName: any;
  registerUserName: any;
  myImageUrl: any;
  otherImageUrl: any;
  authorId: number=0;

  constructor(private chatService: ChatService, private imageService: UploadImageService) { 
    this.message = new Message();
    this.registerUserName = localStorage.getItem('registerUser');
    this.getImageFromService(this.registerUserName, true);
  }

  ngOnInit(): void {
    this.currentUserName = localStorage.getItem('currentUser');

    this.chatService.getChatId(this.registerUserName, this.currentUserName).subscribe(
      (res: any) => {
        this.currentChatID = res;
        this.showMessage(this.currentChatID, this.currentUserName);
      },
      err => {
        console.log(err);
      },
    );

    this.chatService.getAllChats().subscribe(
      (res: any) => {
        this.chats = res;
      },
      err => {
        console.log(err);
      },
    );

  }

arr() {
  
}

  showMessage(chatId: number, chatName: string) {
    this.currentChatID = chatId;
    this.currentChatName = chatName;
    this.chatService.getAllMessages(chatId).subscribe(
      (res: any) => {
        this.messages = res.messages;
        this.authorId = res.id;
        this.getImageFromService(this.currentChatName, false)
      },
      err => {
        console.log(err);
      },
    );
  }

  sendMessage() {
    this.message.chatId = this.currentChatID;
    if(this.message?.content) {
      this.chatService.addMessage(this.message).subscribe(
        (res: any) => {
          this.showMessage(this.currentChatID, this.currentChatName);
          this.message.content = "";
        },
        err => {
          console.log(err);
        },
      )
    }
  }

getImageFromService(userName: string, isMyFoto: boolean) {
this.imageService.downloadFile(userName).subscribe(data => {
 switch (data.type) {
   case HttpEventType.DownloadProgress:
     break;
     case HttpEventType.Response:
       const downloadedFile = new Blob([data.body as BlobPart], { type: data.body?.type });
       this.createImageFromBlob(downloadedFile, isMyFoto);
       break;
}
}, error => {
  if(isMyFoto) {
    this.myImageUrl = "https://bootdey.com/img/Content/avatar/avatar7.png";
  } else {
    this.otherImageUrl = "https://bootdey.com/img/Content/avatar/avatar7.png";
  }
 console.log(error);
});
}

createImageFromBlob(image: Blob, isMyFoto: boolean) {
  let reader = new FileReader();
  reader.addEventListener("load", () => {
    if(isMyFoto) {
      this.myImageUrl = reader.result;
    } else {
      this.otherImageUrl = reader.result;
    }
  }, false);

  if (image) {
     reader.readAsDataURL(image);
  }
}
}


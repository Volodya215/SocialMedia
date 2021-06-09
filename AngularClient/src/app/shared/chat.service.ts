import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable()
export class ChatService {

	constructor(private http: HttpClient){
	}
    readonly BaseURI = 'http://localhost:7609/api';
    public messageAdded_Observable = new Subject();

    getAllChats() {
        return this.http.get(this.BaseURI + "/Chat/AllUserChats");
    }

    getAllMessages(chatId: number) {
        return this.http.get(this.BaseURI + "/Chat/" + chatId + "/AllMessages");
    }

    addMessage(message: Message) {
        return this.http.post(this.BaseURI + '/Chat/AddMessage', message);
    }

    getChatId(firstUsername: string, secondUsername: string) {
        return this.http.get(this.BaseURI + '/Chat/GetChatId/' + firstUsername + "/" + secondUsername);
    }

}

export class Message {
	constructor(){
		this.chatId = 0;
		this.content = '';
	}
	public chatId;
	public content;
}

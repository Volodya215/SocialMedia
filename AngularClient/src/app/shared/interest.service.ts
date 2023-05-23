import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable()
export class InterestsService {

	constructor(private http: HttpClient){
	}
    readonly BaseURI = 'http://localhost:7609/api';
    public interestAdded_Observable = new Subject();

    getAllInterests() {
        return this.http.get(this.BaseURI + '/Interests/All');
    }

    getAllUserInterests(userName: string) {
        return this.http.get(this.BaseURI + '/Interests/' + userName);
    }

    addUserInterest(userinterest: UserInterest) {
        return this.http.post(this.BaseURI + '/Interests/AddUserInterest', userinterest);
    }

    deleteInterest(userName: string, interestId: number) {
        return this.http.delete(this.BaseURI + '/Interests/' + userName + '/' + interestId);
    }
}

export class Interest {
	constructor(){
		this.interestId = 0;
		this.name = '';
	}
	public interestId;
	public name;
}

export class UserInterest {
    constructor(){
		this.interestId = 0;
		this.userName = '';
	}
	public interestId;
	public userName;
}

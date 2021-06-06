import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject }    from 'rxjs';

@Injectable()
export class PostService {

	constructor(private http: HttpClient){
	}
    readonly BaseURI = 'http://localhost:7609/api';
    public postAdded_Observable = new Subject();

    addPost(post: Post) {
        return this.http.post(this.BaseURI + '/Post', post);
    }

    getAllPost(userName: string) {
        return this.http.get(this.BaseURI + "/Post/" + userName);
    }

    notifyPostAddition(){
        this.postAdded_Observable.next();
    }

}

export class Post {
	constructor(){
		this.topic = '';
		this.content = '';
        this.userId = '';
	}
	public topic;
	public content;
    public userId;
}
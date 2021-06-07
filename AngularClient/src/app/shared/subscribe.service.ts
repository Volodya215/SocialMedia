import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject }    from 'rxjs';

@Injectable()
export class SubscribeService {

	constructor(private http: HttpClient){
	}
    readonly BaseURI = 'http://localhost:7609/api';
    public subscribeAdded_Observable = new Subject();
	public guestPageChanged_Observable = new Subject();

	public isShowFollowers: boolean = true;

    isFriend(bloggerUserName: string, subscriberUserName: string) {
        return this.http.get(this.BaseURI + '/BloggerSubscriber/isFriends/' + bloggerUserName + '/' + subscriberUserName);
    }

	follow(model: Subscribe) {
		return this.http.post(this.BaseURI + '/BloggerSubscriber', model);
	}

	unsubscribe(bloggerUserName: string, subscriberUserName: string) {
		return this.http.delete(this.BaseURI + '/BloggerSubscriber/' + bloggerUserName + '/' + subscriberUserName);
	}

	getAllFollowers(userName: string) {
		return this.http.get(this.BaseURI + '/BloggerSubscriber/' + userName + '/followers');
	}

	getAllFollowing(userName: string) {
		return this.http.get(this.BaseURI + '/BloggerSubscriber/' + userName + '/following');
	}

	notifySubscribeAddition(){
        this.subscribeAdded_Observable.next();
    }

	notifyGuestPageChange(){
        this.guestPageChanged_Observable.next();
    }
}

export class Subscribe {
	constructor(){
		this.bloggerUserName = '';
		this.subscriberUserName = '';
	}
	public bloggerUserName;
	public subscriberUserName;
}
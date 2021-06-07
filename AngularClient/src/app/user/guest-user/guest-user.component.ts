import { Component, OnInit } from '@angular/core';
import { SubscribeService } from 'src/app/shared/subscribe.service';


@Component({
  selector: 'app-guest-user',
  templateUrl: './guest-user.component.html',
  styleUrls: ['./guest-user.component.css']
})
export class GuestUserComponent implements OnInit {

  constructor(private service: SubscribeService) {
   }

  ngOnInit(): void {
    this.service.guestPageChanged_Observable.subscribe(res => {
      window.location.reload();
    });
  }
}

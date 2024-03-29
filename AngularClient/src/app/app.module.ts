import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { from } from 'rxjs';
import { UserService } from './shared/user.service';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { MainPageComponent } from './user/main-page/main-page.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { ShowPostComponent } from './show-post/show-post.component';
import { AddPostComponent } from './add-post/add-post.component';
import { PostService } from './shared/post.service';
import { InterestsService } from './shared/interest.service';
import { UserProfileComponent } from './user/user-profile/user-profile.component';
import { SubscribeService } from './shared/subscribe.service';
import { ListUsersComponent } from './user/list-users/list-users.component';
import { GuestUserComponent } from './user/guest-user/guest-user.component';
import { NavigationPanelComponent } from './navigation-panel/navigation-panel.component'
import { FooterComponent } from './footer/footer.component'
import { FilterPipe } from './table.pipe';
import { EditProfileComponent } from './user/edit-profile/edit-profile.component';
import { UploadImageService } from './shared/upload-image.service';
import { UserChatsComponent } from './chat/user-chats/user-chats.component';
import { ChatService } from './shared/chat.service';
import { UserManagerComponent } from './admin-panel/user-manager/user-manager.component';
import { ChatManagerComponent } from './admin-panel/chat-manager/chat-manager.component';


@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent,
    HomeComponent,
    MainPageComponent,
    AdminPanelComponent,
    ForbiddenComponent,
    ShowPostComponent,
    AddPostComponent,
    UserProfileComponent,
    ListUsersComponent,
    GuestUserComponent,
    NavigationPanelComponent,
    FooterComponent,
    FilterPipe,
    EditProfileComponent,
    UserChatsComponent,
    UserManagerComponent,
    ChatManagerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    FormsModule
  ],
  providers: [UserService, PostService, InterestsService, SubscribeService, UploadImageService, ChatService,
  {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }

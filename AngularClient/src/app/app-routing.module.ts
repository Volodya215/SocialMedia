import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { ChatManagerComponent } from './admin-panel/chat-manager/chat-manager.component';
import { UserManagerComponent } from './admin-panel/user-manager/user-manager.component';
import { AuthGuard } from './auth/auth.guard';
import { UserChatsComponent } from './chat/user-chats/user-chats.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { HomeComponent } from './home/home.component';
import { EditProfileComponent } from './user/edit-profile/edit-profile.component';
import { GuestUserComponent } from './user/guest-user/guest-user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';
import { MainPageComponent } from './user/main-page/main-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/user/login', pathMatch: 'full' },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
  {path: 'home', component: HomeComponent, canActivate:[AuthGuard] },
  {path: 'main-page', component: MainPageComponent, canActivate:[AuthGuard] },
  {path: 'forbidden', component: ForbiddenComponent},
  {path: 'adminpanel', component: AdminPanelComponent, canActivate:[AuthGuard], data: {permittedRoles: ['Admin']},
    children: [
      { path: 'usermanagement', component: UserManagerComponent },
      { path: 'chatmanagement', component: ChatManagerComponent }
    ]
  },
  {path: 'guest', component: GuestUserComponent, canActivate:[AuthGuard]},
  {path: 'editProfile', component: EditProfileComponent, canActivate:[AuthGuard]},
  {path: 'chats', component: UserChatsComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

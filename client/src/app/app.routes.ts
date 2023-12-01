import { Routes } from '@angular/router';
import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { NoAccessComponent } from './components/no-access/no-access.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ChatComponent } from './components/chat/chat.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'home', component: HomeComponent},
    {path: 'account/register', component: RegisterComponent},
    {path: 'account/login', component: LoginComponent},
    {path: 'no-access', component: NoAccessComponent},
    {path: '**', component: NotFoundComponent},
    {path: 'chat', component: ChatComponent}
];

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './pages/about/about.component';
import { AccountComponent } from './pages/account/account.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { NoteComponent } from './pages/note/note.component';
import { SignupComponent } from './pages/signup/signup.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "index", component: HomeComponent },
  { path: "login", component: LoginComponent },
  { path: "signup", component: SignupComponent },
  { path: "sobre", component: AboutComponent },
  { path: "about", component: AboutComponent },
  { path: "account", component: AccountComponent },
  { path: "note", component: NoteComponent },
  //{ path: "", component:},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

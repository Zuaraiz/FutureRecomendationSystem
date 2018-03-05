import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';



const routes: Routes = [
{path: '', redirectTo: '/dashboard', pathMatch: 'full'},
    { path: 'signIn',  component: SignInComponent},
    { path: 'signUp',  component: SignUpComponent },
    {path: 'dashboard',  component:DashboardComponent,
	children: [
		   {
		      path: 'home',
		      component: HomeComponent
		   },
		    {
		      path: 'profile',
		      component: ProfileComponent
		   },
		    {
		      path: 'editProfile',
		      component: EditProfileComponent
		    }
		 ]		}] 
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
<<<<<<< HEAD
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule,
} from '@angular/material';
=======
>>>>>>> 9685da2a19a15a8662391ad77a32d3084ceb9b55



import { AppComponent } from './app.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { AppRoutingModule } from './/app-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
<<<<<<< HEAD
import { ProfileComponent, HobbyDialog } from './profile/profile.component';

import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { AuthService } from './auth.service';
import { UserService } from './user.service';
=======
import { ProfileComponent } from './profile/profile.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { AuthService } from './auth.service';



>>>>>>> 9685da2a19a15a8662391ad77a32d3084ceb9b55



@NgModule({
  exports: [
    
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
  ]
})
export class DemoMaterialModule { }


@NgModule({
  entryComponents: [EditProfileComponent,
    HobbyDialog],
  declarations: [
    AppComponent,
    SignInComponent,
    SignUpComponent,
    DashboardComponent,
    HomeComponent,
    ProfileComponent,
    EditProfileComponent,
<<<<<<< HEAD
    HobbyDialog
    
    
    
=======
>>>>>>> 9685da2a19a15a8662391ad77a32d3084ceb9b55
   
  ],
  imports: [
    BrowserModule,
      AppRoutingModule,
<<<<<<< HEAD
    FormsModule,
    BrowserAnimationsModule,
    DemoMaterialModule
    
=======
      FormsModule,
      HttpModule
>>>>>>> 9685da2a19a15a8662391ad77a32d3084ceb9b55

 
  ],
  providers: [
<<<<<<< HEAD
    AuthService
    , UserService
=======
      AuthService 
>>>>>>> 9685da2a19a15a8662391ad77a32d3084ceb9b55
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

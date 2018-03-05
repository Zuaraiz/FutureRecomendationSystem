import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  animal: string='asss';
  name: string = 'asss';

  constructor(public dialog: MatDialog) { }
  openDialog(): void {
    let dialogRef = this.dialog.open(HobbyDialog, {
      
      data: { name: this.name, animal: this.animal }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.animal = result;
    });
  }


  ngOnInit() {
  }

}
@Component({
 
  templateUrl: './hobbyDialog.html',
})
export class HobbyDialog implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<HobbyDialog>) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
  ngOnInit() {
  }
}

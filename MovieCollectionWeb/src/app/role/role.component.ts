import { Component, Input, OnInit, ViewChild } from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { Role } from 'app/shared/view-entity/role';
import { Guid } from 'guid-typescript';
import { RestApiService } from '../../app/shared/rest-api.service';

declare var $: any;

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})

export class RoleComponent implements OnInit {
  displayedColumns: string[] = ['RoleId', 'RoleName'];
  dataSource = new MatTableDataSource<Role>();

  constructor(public restApi: RestApiService) { }
  @ViewChild(MatPaginator) paginator: MatPaginator;


  ngOnInit(): void {
    if (localStorage.getItem("Token") === null) 
    {
      //this.navBar.showLogin();
      this.showNotification('bottom','right','SignIn First','warning')
    }
    else
    {
      this.restApi.QueryRoles().subscribe((data: Role[]) =>{
        this.dataSource.data = data;
        console.log ("data ", data); 
      });
    }
  }
  ngAfterViewInit() {
    
    this.dataSource.paginator = this.paginator;
  }

  showNotification(from, align,message,type){
    //const type = ['','info','success','warning','danger'];
    $.notify({
        icon: "notifications",
        message: message

    },{
        type: type,
        timer: 4000,
        placement: {
            from: from,
            align: align
        },
        template: '<div data-notify="container" class="col-xl-4 col-lg-4 col-11 col-sm-4 col-md-4 alert alert-{0} alert-with-icon" role="alert">' +
          '<button mat-button  type="button" aria-hidden="true" class="close mat-button" data-notify="dismiss">  <i class="material-icons">close</i></button>' +
          '<i class="material-icons" data-notify="icon">notifications</i> ' +
          '<span data-notify="title">{1}</span> ' +
          '<span data-notify="message">{2}</span>' +
          '<div class="progress" data-notify="progressbar">' +
            '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
          '</div>' +
          '<a href="{3}" target="{4}" data-notify="url"></a>' +
        '</div>'
    });
}

}
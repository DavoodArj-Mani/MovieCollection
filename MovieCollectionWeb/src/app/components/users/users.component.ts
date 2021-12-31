import { Component, OnInit, ViewChild } from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { Router } from '@angular/router';
import { Notify } from 'app/shared/notification';
import { RestApiService } from 'app/shared/rest-api.service';
import { User } from 'app/shared/view-entity/user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  displayedColumns: string[] = ['UserId','UserName','action'];
  dataSource = new MatTableDataSource<User>();
  user = new User;
  

  constructor(public restApi: RestApiService,private router: Router,private notification: Notify) { }
  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit(): void {
    if (localStorage.getItem("Token") === null) 
    {
      //this.navBar.showLogin();
      this.notification.showNotification('bottom','right','SignIn First','warning')
    }
    else
    {
      this.queryUser();
    }
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  queryUser()
  {
    this.restApi.QueryUsers().subscribe((data: User[]) =>{
      this.dataSource.data = data;
    });
  }
  
  openDialog() {

  }
  queryUserByName(userName){
    
    this.restApi.QueryUserByName(userName).subscribe((data) =>{
      this.dataSource.data = data;
    });
  }
  clearUserSearch()
  {
    this.queryUser();
  }

}

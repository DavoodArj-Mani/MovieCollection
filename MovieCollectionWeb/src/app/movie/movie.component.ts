import { Component, OnInit, ViewChild } from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { RestApiService } from 'app/shared/rest-api.service';
import { Movie } from 'app/shared/view-entity/movie';

declare var $: any;
@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {
  displayedColumns: string[] = ['MovieId', 'MovieName','action'];
  dataSource = new MatTableDataSource<Movie>();
  movie = new Movie;

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
      this.queryMovie();
    }
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  queryMovie()
  {
    this.restApi.QueryMovies().subscribe((data: Movie[]) =>{
      this.dataSource.data = data;
      //console.log ("data ", data); 
    });
  }
  createMovie(movieName)
  {
    console.log ("movieName ", movieName); 
    if (localStorage.getItem("Token") === null) 
    {
      //this.navBar.showLogin();
      this.showNotification('bottom','right','SignIn First','warning')
    }
    else
    {
      this.movie.MovieName = movieName;
      this.restApi.CreateMovie(this.movie).subscribe((data) =>{
        this.showNotification('bottom','right','Movie successfully created.','success');
        this.queryMovie();
      });
    }
  }
  openDialog(action,obj) {

    if (action == "Delete")
    {
      this.restApi.DeleteMovie(obj.movieId).subscribe((data) =>{
        if(data)
        {
          this.showNotification('bottom','right','Movie successfully Deleted.','success');
          this.queryMovie();
        }
        else
        {
          this.showNotification('bottom','right','Delete movie was not successfull.','warning');
        }
        
      });
    }
  }
  searchMovie(movieName){
    
    this.restApi.QueryByName(movieName).subscribe((data) =>{
      this.dataSource.data = data;
    });
  }
  clearMovieSearch()
  {
    this.queryMovie();
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

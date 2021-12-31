import { Component, OnInit, ViewChild } from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { Router } from '@angular/router';
import { Notify } from 'app/shared/notification';
import { RestApiService } from 'app/shared/rest-api.service';
import { Movie } from 'app/shared/view-entity/movie';


@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {
  displayedColumns: string[] = ['MovieName','IMDB_Score','action'];
  dataSource = new MatTableDataSource<Movie>();
  movie = new Movie;
  

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
    });
  }
  createMovie()
  {
    if (localStorage.getItem("Token") === null) 
    {
      this.notification.showNotification('bottom','right','SignIn First','warning')
    }
    else
    {
      this.router.navigate(['Movie']);
    }
  }
  openDialog(action,obj) {

    if (action == "Delete")
    {
      this.restApi.DeleteMovie(obj.movieId).subscribe((data) =>{
        if(data)
        {
          this.notification.showNotification('bottom','right','Movie successfully Deleted.','success');
          this.queryMovie();
        }
        else
        {
          this.notification.showNotification('bottom','right','Delete movie was not successfull.','warning');
        }
        
      });
    }
    else if(action == "Update")
    {
      this.router.navigate(['Movie',{movieId: obj.movieId}]);
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

}

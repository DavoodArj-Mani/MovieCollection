import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RestApiService } from 'app/shared/rest-api.service';
import { Collection } from 'app/shared/view-entity/collection';
import { CollectionMovie } from 'app/shared/view-entity/collection-movie';
import { Movie } from 'app/shared/view-entity/movie';
import { SelectedCollectionMovie } from 'app/shared/view-entity/selected-collection-movie';
import { Guid } from 'guid-typescript';

declare var $: any;
@Component({
  selector: 'app-my-collection',
  templateUrl: './my-collection.component.html',
  styleUrls: ['./my-collection.component.css']
})
export class MyCollectionComponent implements OnInit {
  showSelectedMovie: boolean = false;
  selectedCollectionId: Guid;
  collection: Collection = new Collection;
  selectedCollectionMovie: SelectedCollectionMovie[] = [];

  myCollectionDisplayedColumns: string[] = [ 'CollectionName','action'];
  myCollectionDataSource = new MatTableDataSource<Collection>();

  myMovieDisplayedColumns: string[] = ['CollectionName', 'MovieName','action'];
  myMovieDataSource = new MatTableDataSource<SelectedCollectionMovie>();

  movieDisplayedColumns: string[] = [ 'MovieName','action'];
  movieDataSource = new MatTableDataSource<Movie>();

  constructor(public restApi: RestApiService) { }
  @ViewChild(MatPaginator) collectionPaginator: MatPaginator;
  @ViewChild(MatPaginator) moviePaginator: MatPaginator;
  

  ngOnInit(): void {
    this.showSelectedMovie = false;
    if (localStorage.getItem("Token") === null) 
    {
      //this.navBar.showLogin();
      this.showNotification('bottom','right','SignIn First','warning')
    }
    else
    {
      this.queryMyCollection();
    }
  }
  ngAfterViewInit() {
    this.myCollectionDataSource.paginator = this.collectionPaginator;
    this.movieDataSource.paginator = this.moviePaginator;
  }
  queryMyCollection()
  {
    this.restApi.QueryMyCollections().subscribe((data: Collection[]) =>{
      this.myCollectionDataSource.data = data;
    });
  }
  selectedCollection(obj)
  {
    this.myMovieDataSource = new MatTableDataSource<SelectedCollectionMovie>();
    this.selectedCollectionMovie = [];
    
    console.log ("obj ", obj); 
    obj.movies.forEach((item, index) => {
      console.log ("item ", item); 
      const _selectedCollectionMovie = {} as SelectedCollectionMovie;
      _selectedCollectionMovie.CollectionId = obj.collectionId;
      _selectedCollectionMovie.CollectionName = obj.collectionName;
      _selectedCollectionMovie.MovieId = item.movieId;
      _selectedCollectionMovie.MovieName = item.movieName;
      this.selectedCollectionMovie.push(_selectedCollectionMovie);
    });
    console.log ("selectedCollectionMovie ", this.selectedCollectionMovie); 
    this.myMovieDataSource.data = this.selectedCollectionMovie;
  }
  deleteMyCollectionMovie(obj)
  {
    console.log ("obj ", obj); 
  }
  searchMyCollection(collectionName)
  {
    this.restApi.QueryMyCollectionsByName(collectionName).subscribe((data: Collection[]) =>{
      this.myCollectionDataSource.data = data;
    });
  }
  addCollectionMovie(obj)
  {
    
    const collectionMovie = {} as CollectionMovie;
    collectionMovie.CollectionId = this.selectedCollectionId;
    collectionMovie.MovieId = obj.movieId;

    this.restApi.CreateCollectionMovie(collectionMovie).subscribe((data) =>{
      this.showNotification('bottom','right','Movie successfully Added.','success');
      this.queryMyCollection();
      this.myMovieDataSource = new MatTableDataSource<SelectedCollectionMovie>();
      this.hideSelectMovie();
    });
  
  }
  showSelectMovie(obj)
  {
    this.selectedCollectionId = obj.collectionId;
    this.showSelectedMovie = true;
    this.restApi.QueryMovies().subscribe((data: Movie[]) =>{
      this.movieDataSource.data = data;
      console.log ("data ", data); 
    });
  }
  hideSelectMovie()
  {
    this.showSelectedMovie = false;
  }

  CreateMyCollection(collectionName)
  {
    if (localStorage.getItem("Token") === null) 
    {
      //this.navBar.showLogin();
      this.showNotification('bottom','right','SignIn First','warning')
    }
    else
    {
    
      this.collection.CollectionName =  collectionName;

      this.restApi.CreateCollection(this.collection).subscribe((data) =>{
        this.showNotification('bottom','right','Collection successfully created.','success');
        this.queryMyCollection();
      });
    }
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

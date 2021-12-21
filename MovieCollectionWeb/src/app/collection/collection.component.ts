import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RestApiService } from 'app/shared/rest-api.service';
import { Collection } from 'app/shared/view-entity/collection';
import { Movie } from 'app/shared/view-entity/movie';
import { SelectedCollectionMovie } from 'app/shared/view-entity/selected-collection-movie';
declare var $: any;
@Component({
  selector: 'app-collection',
  templateUrl: './collection.component.html',
  styleUrls: ['./collection.component.css']
})
export class CollectionComponent implements OnInit {
  collectionDisplayedColumns: string[] = ['CollectionId', 'CollectionName','action'];
  collectionDataSource = new MatTableDataSource<Collection>();

  movieDisplayedColumns: string[] = ['CollectionId', 'CollectionName','MovieId', 'MovieName'];
  movieDataSource = new MatTableDataSource<SelectedCollectionMovie>();

  selectedCollectionMovie: SelectedCollectionMovie[] = [];
  
  

  constructor(public restApi: RestApiService) { }
  @ViewChild(MatPaginator) collectionPaginator: MatPaginator;

  ngOnInit(): void {
    if (localStorage.getItem("Token") === null) 
    {
      //this.navBar.showLogin();
      this.showNotification('bottom','right','SignIn First','warning')
    }
    else
    {
      this.queryCollection();
    }
  }
  ngAfterViewInit() {
    this.collectionDataSource.paginator = this.collectionPaginator;
  }

 
  queryCollection()
  {
    this.restApi.QueryCollection().subscribe((data: Collection[]) =>{
      this.collectionDataSource.data = data;
      //console.log ("data ", data); 
    });
  }
  searchCollection(collectionName)
  {
    this.restApi.QueryCollectionsByName(collectionName).subscribe((data: Collection[]) =>{
      this.collectionDataSource.data = data;
    });
  }
  selectedCollection(obj) {

    this.movieDataSource = new MatTableDataSource<SelectedCollectionMovie>();
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
    this.movieDataSource.data = this.selectedCollectionMovie;
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

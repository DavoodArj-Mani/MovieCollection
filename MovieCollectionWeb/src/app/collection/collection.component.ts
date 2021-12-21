import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RestApiService } from 'app/shared/rest-api.service';
import { Collection } from 'app/shared/view-entity/collection';
import { Movie } from 'app/shared/view-entity/movie';
declare var $: any;
@Component({
  selector: 'app-collection',
  templateUrl: './collection.component.html',
  styleUrls: ['./collection.component.css']
})
export class CollectionComponent implements OnInit {
  collectionDisplayedColumns: string[] = ['CollectionId', 'CollectionName','action'];
  collectionDataSource = new MatTableDataSource<Collection>();

  showMoviesModal: boolean;
  movieDisplayedColumns: string[] = ['MovieId', 'MovieName'];
  movieDataSource = new MatTableDataSource<Movie>();
  

  constructor(public restApi: RestApiService) { }
  @ViewChild(MatPaginator) collectionPaginator: MatPaginator;
  @ViewChild(MatPaginator) moviePaginator: MatPaginator;

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
    this.movieDataSource.paginator = this.moviePaginator;
  }

  showMovies()
  {
    this.showMoviesModal = true; 
  }
  hideMovies()
  {
    this.showMoviesModal = false;
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

  }
  openDialog(action,obj) {

    if (action == "More")
    {
      this.showMovies();
      this.restApi.QueryCollectionMovies(obj.CollectionId).subscribe((data) =>{
        this.movieDataSource.data = data;
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

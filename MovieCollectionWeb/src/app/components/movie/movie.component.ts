import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { image } from 'app/shared/image.const';
import { Notify } from 'app/shared/notification';
import { RestApiService } from 'app/shared/rest-api.service';
import { SharedService } from 'app/shared/shared-service';
import { Movie } from 'app/shared/view-entity/movie';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})

export class MovieComponent implements OnInit {
  movieForm: FormGroup;
  movie: Movie;
  isNew = false;
  movieId;
  

  color = 'primary';
  mode = 'indeterminate';
  value = 30;
  displayProgressSpinner = false;

  @ViewChild('fileInput') fileInput: ElementRef;
  fileAttr = 'Choose File';
  imageSource;
  imgBase64Path;

  constructor(public sharedService: SharedService,
    public restApi: RestApiService,
    private notification: Notify, 
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.imageSource = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${image}`);

    this.movieForm = this.formBuilder.group({
      movieId: [{value: '', disabled: true}],
      movieName: [''],
      director: [''],
      writers: [''],
      stars: [''],
      imdb_score: [''],
    })
    
    this.route.params.subscribe( params => 
      this.movieId = params['movieId']
    );
    if(this.movieId )
    {
      this.isNew = true;
      this.restApi.QueryMovieById(this.movieId ).subscribe((data) =>{
        this.movie = data;
        console.log ("movie", this.movie); 

        this.movieForm = this.formBuilder.group({
          movieId: [{value: this.movie.movieId, disabled: true}],
          movieName: this.movie.movieName,
          director: this.movie.director,
          writers: this.movie.writers,
          stars: this.movie.stars,
          imdb_score: this.movie.imdB_Score,
        })
        if(this.movie.image)
          this.imageSource = this.sanitizer.bypassSecurityTrustResourceUrl(`${this.movie.image}`);
      });
    }
    else{
      console.log ("New"); 
    }
  }
  get f() { return this.movieForm.controls; }
  
  UpdateForm()
  {
    this.movieForm = this.formBuilder.group({
      movieId: [{value: this.movie.movieId, disabled: true}],
      movieName: this.movie.movieName,
      director: this.movie.director,
      writers: this.movie.writers,
      stars: this.movie.stars,
      imdb_score: this.movie.imdB_Score,
    })
    this.imageSource = this.sanitizer.bypassSecurityTrustResourceUrl(`${this.movie.image}`);
  }
  submitForm() {
    this.movie = new Movie();
    if(this.movieForm.get('movieId').value)
      this.movie.movieId = this.movieForm.get('movieId').value;
    if(this.movieForm.get('movieName').value)
      this.movie.movieName = this.movieForm.get('movieName').value;
    if(this.movieForm.get('director').value)
      this.movie.director = this.movieForm.get('director').value
    if(this.movieForm.get('writers').value)
      this.movie.writers = this.movieForm.get('writers').value
    if(this.movieForm.get('stars').value)
      this.movie.stars = this.movieForm.get('stars').value
    if(this.movieForm.get('imdb_score').value)
      this.movie.imdB_Score = this.movieForm.get('imdb_score').value

    if (this.imgBase64Path)
      this.movie.image = this.imgBase64Path;

    console.log ("movie", this.movie); 
    this.displayProgressSpinner = true;
    if(this.movieId != null) // Update
    {
      this.restApi.updateMovie(this.movie).subscribe((data) =>{
        this.movie = data;
        this.UpdateForm();
        this.notification.showNotification('bottom','right','Movie successfully Updated.','success');
        this.displayProgressSpinner = false;
      });
      
    }
    else // Create
    {
      
      this.restApi.CreateMovie(this.movie).subscribe((data) =>{
        this.movie = data;
        this.UpdateForm();
        this.notification.showNotification('bottom','right','Movie successfully Created.','success');
        this.displayProgressSpinner = false;
      });
      
    }
    
  }
  


  uploadFileEvt(imgFile: any) {
    if (imgFile.target.files && imgFile.target.files[0]) {
      this.fileAttr = '';
      Array.from(imgFile.target.files).forEach((file: File) => {
        this.fileAttr += file.name + ' - ';
      });

      // HTML5 FileReader API
      let reader = new FileReader();
      reader.onload = (e: any) => {
        let image = new Image();
        image.src = e.target.result;
        image.onload = rs => {
          this.imgBase64Path = e.target.result;
          this.imageSource = this.sanitizer.bypassSecurityTrustResourceUrl(`${this.imgBase64Path }`);
        };
      };
      reader.readAsDataURL(imgFile.target.files[0]);
      
      // Reset if duplicate image uploaded again
      this.fileInput.nativeElement.value = "";
    } else {
      this.fileAttr = 'Choose File';
    }
  }
}

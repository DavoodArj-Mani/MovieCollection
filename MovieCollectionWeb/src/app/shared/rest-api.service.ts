import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { User } from './view-entity/user';
import { Role } from './view-entity/role';
import { LoginResp } from './view-entity/login-resp';
import { Movie } from './view-entity/movie';
import { Collection } from './view-entity/collection';
import { CollectionMovie } from './view-entity/collection-movie';
import { NavbarComponent } from 'app/components/navbar/navbar.component';

@Injectable({
  providedIn: 'root'
})

export class RestApiService {
  
  // Define API
  //apiURL = 'http://localhost:56600';
  apiURL = "http://localhost:5000";

  constructor(private http: HttpClient/*,private navBar: NavbarComponent*/) { }

  /*========================================
    CRUD Methods for consuming RESTful API
  =========================================*/
  //localStorage.removeItem('Token');
  //localStorage.getItem('Token')
  //if (localStorage.getItem("Token") === null) 
  // Http Options
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  // HttpClient API post() method => Login
  Login(User): Observable<LoginResp> {
    return this.http.post<LoginResp>(this.apiURL + '/api/Authentication/Login', JSON.stringify(User), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }  
  // HttpClient API post() method => SignUp
  SignUp(User): Observable<User> {
    return this.http.post<User>(this.apiURL + '/api/Authentication/SignUp', JSON.stringify(User), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  //----------------------------------------------------------------------------------------------------

  // HttpClient API get() method => QueryRoles
  QueryRoles(): Observable<Role[]> 
  {
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
      
    console.log ("token ", localStorage.getItem('Token')); 
    console.log ("httpOptions ", this.httpOptions); 
    return this.http.get<Role[]>(this.apiURL + '/api/Role/QueryAll',this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  //----------------------------------------------------------------------------------------------------
   // HttpClient API Get() method => QueryMovies
  QueryMovies(): Observable<Movie[]> 
  {
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.get<Movie[]>(this.apiURL + '/api/Movie/QueryAll',this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  QueryByName(MovieName): Observable<Movie[]> 
  {
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.get<Movie[]>(this.apiURL + '/api/Movie/QueryByName/'+ MovieName,this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  // HttpClient API post() method => CreateMovie
  CreateMovie(Movie): Observable<Movie> {
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.post<Movie>(this.apiURL + '/api/Movie/Create', JSON.stringify(Movie), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  // HttpClient API delete() method => DeleteMovie
  DeleteMovie(Guid){
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.delete<Boolean>(this.apiURL + '/api/Movie/Delete/' + Guid, this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  //----------------------------------------------------------------------------------------------------
  // HttpClient API Get() method => QueryCollection
  QueryCollection(): Observable<Collection[]> 
  {
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.get<Collection[]>(this.apiURL + '/api/Collection/QueryAll',this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  // HttpClient API Get() method => QueryCollectionMovies
  QueryCollectionMovies(Guid){
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.get<Movie[]>(this.apiURL + '/api/Collection/QueryCollectionMovies/' + Guid, this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  //----------------------------------------------------------------------------------------------------
  // HttpClient API post() method => CreateCollection
  CreateCollection(Collection): Observable<Collection> {
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.post<Collection>(this.apiURL + '/api/Collection/Create', JSON.stringify(Collection), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  QueryCollectionsByName(collectionName){
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.get<Collection[]>(this.apiURL + '/api/Collection/QueryByName/'+ collectionName, this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

  // HttpClient API Get() method => QueryMyCollections
  QueryMyCollections(){
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.get<Collection[]>(this.apiURL + '/api/Collection/QueryMyCollections/', this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  QueryMyCollectionsByName(collectionName){
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.get<Collection[]>(this.apiURL + '/api/Collection/QueryMyCollectionsByName/'+ collectionName, this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

  CreateCollectionMovie(CollectionMovie): Observable<CollectionMovie> 
  {
    if(!this.httpOptions.headers.has('Authorization'))
      this.httpOptions.headers = this.httpOptions.headers.append('Authorization', 'Bearer ' + localStorage.getItem('Token'))
      
    return this.http.post<CollectionMovie>(this.apiURL + '/api/Collection/CreateCollectionMovie', JSON.stringify(CollectionMovie), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }
  

  //----------------------------------------------------------------------------------------------------
  // Error handling 
  handleError(error) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      // Get client-side error
      console.log ("error ", error); 
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      if(error.status == '401')
      {
        localStorage.clear();
        errorMessage = `Error Code: 401\nMessage: Unauthorized`;
      }
      else
      {
        errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      }
      
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
  


/*
  // HttpClient API get() method => Fetch employees list
  getEmployees(): Observable<Employee> {
    return this.http.get<Employee>(this.apiURL + '/employees')
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

  // HttpClient API get() method => Fetch employee
  getEmployee(id): Observable<Employee> {
    return this.http.get<Employee>(this.apiURL + '/employees/' + id)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }  

  // HttpClient API post() method => Create employee
  createEmployee(employee): Observable<Employee> {
    return this.http.post<Employee>(this.apiURL + '/employees', JSON.stringify(employee), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }  

  // HttpClient API put() method => Update employee
  updateEmployee(id, employee): Observable<Employee> {
    return this.http.put<Employee>(this.apiURL + '/employees/' + id, JSON.stringify(employee), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

  // HttpClient API delete() method => Delete employee
  deleteEmployee(id){
    return this.http.delete<Employee>(this.apiURL + '/employees/' + id, this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.handleError)
    )
  }

  
  
*/

}
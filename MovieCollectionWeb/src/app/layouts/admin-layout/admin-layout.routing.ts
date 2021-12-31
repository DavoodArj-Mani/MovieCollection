import { Routes } from '@angular/router';


import { MovieComponent } from 'app/components/movie/movie.component';
import { MyCollectionComponent } from 'app/components/my-collection/my-collection.component';
import { ContactComponent } from 'app/components/contact/contact.component';
import { MoviesComponent } from 'app/components/movies/movies.component';
import { CollectionComponent } from 'app/components/collection/collection.component';
import { Movie } from 'app/shared/view-entity/movie';
import { UsersComponent } from 'app/components/users/users.component';



export const AdminLayoutRoutes: Routes = [
    { path: 'Collections',     component: CollectionComponent},
    { path: 'MyCollection',   component: MyCollectionComponent },
    { path: 'Movies',         component: MoviesComponent},
    { path: 'Movie',          component: MovieComponent,data:Movie},
    { path: 'Contact',        component: ContactComponent },
    { path: 'Users',        component: UsersComponent },
    
];

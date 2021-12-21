import { Routes } from '@angular/router';


import { MovieComponent } from 'app/movie/movie.component';
import { CollectionComponent } from 'app/collection/collection.component';
import { RoleComponent } from 'app/role/role.component';
import { MyCollectionComponent } from 'app/my-collection/my-collection.component';



export const AdminLayoutRoutes: Routes = [
    { path: 'Collection',     component: CollectionComponent},
    { path: 'Movie',          component: MovieComponent},
    { path: 'Role',           component: RoleComponent },
    { path: 'MyCollection',   component: MyCollectionComponent },
    ,
];

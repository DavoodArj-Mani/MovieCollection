import { Guid } from 'guid-typescript';
import { MovieType } from './movie-type';
export class Movie {
    movieId: Guid;
    movieName: string;
    director: string;
    writers: string;
    stars: string;
    imdB_Score: string;
    image: string;
    movieTypes: MovieType[];
 }
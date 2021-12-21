import { Guid } from 'guid-typescript';
import { Movie } from './movie';
export class Collection {
    CollectionId: Guid;
    CollectionName: string;
    CreatedBy: Guid;
    Movies: Movie[];
 }
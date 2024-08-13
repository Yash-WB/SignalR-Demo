import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { SignalRService } from './signalr.service';

export interface MyEntity {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class EntityService {
  private apiUrl = 'http://localhost:7161/api/MyEntities';
  private entitiesSubject = new BehaviorSubject<MyEntity[]>([]);
  entities$ = this.entitiesSubject.asObservable();

  constructor(private http: HttpClient, private signalRService: SignalRService) {
    this.loadEntities();

    this.signalRService.entityUpdated$.subscribe(name => {
      // Refresh the list of entities when a new update is received
      this.loadEntities();
    });
  }

  private loadEntities(): void {
    this.http.get<MyEntity[]>(this.apiUrl).subscribe(entities => {
      this.entitiesSubject.next(entities);
    });
  }

  addEntity(entity: MyEntity): Observable<MyEntity> {
    return this.http.post<MyEntity>(this.apiUrl, entity);
  }
}

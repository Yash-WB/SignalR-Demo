import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  private entityUpdatedSource = new Subject<string>();
  entityUpdated$ = this.entityUpdatedSource.asObservable();

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:7161/myHub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR connection started'))
      .catch(err => console.log('Error while starting SignalR connection: ' + err));
  }

  addItemUpdateListener(): void {
    this.hubConnection.on('ReceiveItemUpdate', (name: string) => {
      this.entityUpdatedSource.next(name);
    });
  }
}

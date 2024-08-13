import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { EntityListFormComponent } from './components/entity-list-form/entity-list-form.component';
import { SignalRService } from './services/signalr.service';

@NgModule({
  declarations: [
    AppComponent,
    EntityListFormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [SignalRService],
  bootstrap: [AppComponent]
})
export class AppModule { }

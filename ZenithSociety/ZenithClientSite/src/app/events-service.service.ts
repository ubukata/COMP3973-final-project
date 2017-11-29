import { Injectable } from '@angular/core';
import { Event } from '../event';
import { EventsByDate } from '../events-by-date';
import { Headers, Http, Response } from '@angular/http';

@Injectable()
export class EventsServiceService {
  private BASE_URL = 'http://localhost:5000/api/events';
  // private BASE_URL = 'https://vitor-ken-ass2.azurewebsites.net/api/events';
  constructor(private http: Http) { }

  getEvents(dateFrom: string, dateTo: string): Promise<EventsByDate> {
    return this.http.get(`${this.BASE_URL}/${dateFrom}/${dateTo}`)
      .toPromise()
      .then(response => response.json() as EventsByDate)
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}
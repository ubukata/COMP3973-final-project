import { Component, OnInit } from '@angular/core';
import { EventsServiceService } from './events-service.service';
import { EventsByDate } from '../events-by-date';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [EventsServiceService]
})
export class AppComponent implements OnInit {
  eventsByDate: EventsByDate;
  page: number;
  dateFrom: string;
  dateTo: string;

  constructor(
    private eventsService: EventsServiceService
  ) {
    this.page = 0;
  }

  getEvents(): void {
    let current = new Date;
    current.setDate(current.getDate() + (this.page * 7));
    const dateFrom = new Date(current.setDate(current.getDate() - current.getDay()))
      .toISOString().slice(0, 10);
    const dateTo = new Date(current.setDate(current.getDate() - current.getDay() + 6))
      .toISOString().slice(0, 10);

    this.dateFrom = dateFrom;
    this.dateTo = dateTo;

    this.eventsService.getEvents(dateFrom, dateTo)
    .then(eventsByDate => this.eventsByDate = eventsByDate);
  }

  previous(): void {
    this.page--;
    this.getEvents();
  }

  next(): void {
    this.page++;
    this.getEvents();
  }

  ngOnInit(): void {
    this.getEvents();
  }
}

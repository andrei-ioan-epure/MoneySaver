import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-full-offer',
  templateUrl: './full-offer.component.html',
  styleUrls: ['./full-offer.component.scss']
})
export class FullOfferComponent implements OnInit{
  @Input() image?:string;
  @Input() title?: string;
  @Input() city?:string;
  @Input() expiration?:Date;
  @Input() code?:string;
  @Input() category?:string;
  @Input() author?:string;
  @Input() posted?:Date;
  @Input() comment?:string;


  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}



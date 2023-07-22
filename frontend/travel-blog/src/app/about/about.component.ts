import { Component, OnInit } from '@angular/core';
import { Cards } from './model/card';
import { CardService } from '../services/card.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit{
  cards?: Cards;
  
  constructor(private readonly cardService: CardService){}
  
  ngOnInit(): void {
    this.cards=this.cardService.getCards();
  }
}

import { Injectable } from '@angular/core';
import { Cards } from '../about/model/card'; 

@Injectable({
  providedIn: 'root'
})
export class CardService {
  cards:Cards=[
    {
      img:'/assets/images/Andrei.jpg',
      lname:'Epure',
      fname:'Andrei-Ioan',
      description:'A person who finds joy in programming and shares a passion for playing the guitar.'
    },
    {
      img:'/assets/images/Sebi.jpg',
      lname:'Miron',
      fname:'Sebastian',
      description:'I am a curious person who finds immense joy in exploring the world of computer science.'
    },
    {
      img:'/assets/images/Axana.jpg',
      lname:'Diaconu',
      fname:'Axana-Marinela',
      description:'I am a sociable and energetic person. I like to read, travel and I am excited to meet new people.'
    },
    {
      img:'/assets/images/Bianca.jpg',
      lname:'Brânzei',
      fname:'Bianca-Gabriela',
      description:'An enthusiast with a problem solving mind. Ready to help and always ready to laugh with.'
    },
    {
      img:'/assets/images/Iustina.jpeg',
      lname:'Bulai',
      fname:'Iustina-Bianca',
      description:'I am an optimistic and energetic person. I like to read, sing, dance and go on hikes.'
    }
  ];

  getCards():Cards{
    return this.cards;
  }
}

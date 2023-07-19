import { Injectable } from '@angular/core';
import { Cards } from '../about/model/card'; 

@Injectable({
  providedIn: 'root'
})
export class CardService {
  cards:Cards=[
    {
      img:'/assets/images/Iustina.jpeg',
      name:'Epure Andrei-Ioan',
      description:'A person who finds joy in programming and shares a passion for playing the guitar.'
    },
    {
      img:'/assets/images/Iustina.jpeg',
      name:'Miron Sebastian',
      description:'I am a curious person who finds immense joy in exploring the world of computer science'
    },
    {
      img:'/assets/images/Iustina.jpeg',
      name:'Diaconu Axana-Marinela',
      description:'I am a sociable and energetic person. I like to travel.'
    },
    {
      img:'/assets/images/Iustina.jpeg',
      name:'Brânzei Bianca-Gabriela',
      description:'An enthusiast with a problem solving mind. Ready to help and always ready to laugh with.'
    },
    {
      img:'/assets/images/Iustina.jpeg',
      name:'Bulai Iustina-Bianca',
      description:'I am an optimistic and energetic person. I like to read, sing and dance. '
    }
  ];

  getCards():Cards{
    return this.cards;
  }
}

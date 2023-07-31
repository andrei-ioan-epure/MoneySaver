import { Component } from '@angular/core';

@Component({
  selector: 'app-contact-response',
  templateUrl: './contact-response.component.html',
  styleUrls: ['./contact-response.component.scss']
})
export class ContactResponseComponent {

  ImagePath:string;
  constructor(){
  this.ImagePath = '/assets/images/thankyou.png';
  }
}

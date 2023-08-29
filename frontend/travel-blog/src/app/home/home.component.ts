import { Component, AfterViewInit } from '@angular/core';
import Swiper from 'swiper';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements AfterViewInit {
  carouselSlides: string[] = [
    '../../assets/images/answear.jpg',
    '../../assets/images/netflix.jpg',
    '../../assets/images/cupio.jpg',
    '../../assets/images/glovo.jpg',
    '../../assets/images/douglas.jpg',
    '../../assets/images/stradivarius.jpg',
    '../../assets/images/spotify.jpg',
    '../../assets/images/tazz.jpg',
    '../../assets/images/fashion-days.jpg',
  ];
  activeSlideIndex: number = 0;
  swiper!: Swiper;
  timerInterval: any;
  autoplayInterval = 1500;

  goToNextSlide(): void {
    this.swiper.slideNext();
  }

  goToPreviousSlide(): void {
    this.swiper.slidePrev();
  }

  ngAfterViewInit() {
    this.swiper = new Swiper('.swiper-container', {
      slidesPerView: 1,
      spaceBetween: 0,
      loop: true,
      effect: 'slide',
      speed: 0,
      on: {
        slideChange: () => {
          this.activeSlideIndex = this.swiper.realIndex;
        },
      },
    });
    this.startAutoplay();
  }

  isActiveSlide(index: number): boolean {
    return index === this.activeSlideIndex;
  }
  goToSlide(slideIndex: number): void {
    this.swiper.slideToLoop(slideIndex);
  }
  startAutoplay(): void {
    this.timerInterval = setInterval(() => {
      this.goToNextSlide();
    }, this.autoplayInterval);
  }

  stopAutoplay(): void {
    clearInterval(this.timerInterval);
  }
}

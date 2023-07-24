import { Component, OnInit } from '@angular/core';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'Login',
  templateUrl: './Login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  title = 'ng-carousel-demo';
  
  images = [
    {title: 'First Slide', short: 'First Slide Short', src: "assets/images/face.jpg"},
    {title: 'Second Slide', short: 'Second Slide Short', src: "assets/images/face-2.jpg"},
    {title: 'Third Slide', short: 'Third Slide Short', src: "assets/images/face-4.jpg"}
  ];
  
  constructor(config: NgbCarouselConfig) {
    config.interval = 3000;
    config.keyboard = true;
    config.pauseOnHover = true;
  }

  ngOnInit() {
  }

}

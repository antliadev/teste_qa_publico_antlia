import { Component, isDevMode, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'MovimentosManuais';


  ngOnInit() {
    if (isDevMode()) {
      console.log('Modo 👋 Dev Antlia!');
    } else {
      console.log('Modo 👋 Prod Antlia!');
    }
  }
}

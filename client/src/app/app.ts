import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { Navbar } from "./shared/components/navbar/navbar";
import { FooterComponent } from "./shared/components/footer/footer.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TooltipModule, Navbar, FooterComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  constructor() {

  }
}

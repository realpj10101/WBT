import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { Player } from './models/player.model';
import { RegisterPlayerService } from './services/register-player.service';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { isPlatformBrowser, isPlatformServer } from '@angular/common';


@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterModule, NavbarComponent, FooterComponent]
})
export class AppComponent {
  registerPlayerService = inject(RegisterPlayerService);
  platformId = inject(PLATFORM_ID)

  allPlayers: Player[] | undefined;

  ngOnInit(): void {
    console.log('PlatformId in OnInit:', this.platformId);
    this.getLocalStorageCurrentValues();
  }

  getLocalStorageCurrentValues(): void {
    let playerString: string | null = null;

    if (isPlatformBrowser(this.platformId)) {
      console.log('PlatformId in method:', this.platformId);
      playerString = localStorage.getItem('player');
    }

    if (playerString) {
      const player: Player = JSON.parse(playerString);

      this.registerPlayerService.setCurrentPlayer(player);
    }
  }
}

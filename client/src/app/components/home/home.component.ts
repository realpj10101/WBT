import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Observable } from 'rxjs';
import { Player } from '../../models/player.model';
import { RegisterPlayerService } from '../../services/register-player.service';
import { PlayerService } from '../../services/player.service';
import { CommonModule } from '@angular/common';
import {MatTableModule} from '@angular/material/table';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatTableModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  playerService = inject(PlayerService);
  registerPlayerService = inject(RegisterPlayerService);

  allPlayers: Player[] | null | undefined;
  allPlayers$: Observable<Player[] | null> | undefined;

  showAllPlayers() {
    this.playerService.getAllPlayers().subscribe({
      next: (players: Player[] | null) => this.allPlayers = players,
      error: err => console.log(err.message),
    });

    this.allPlayers$ = this.playerService.getAllPlayers();
  }

  logOut(): void {
    this.registerPlayerService.logOutPlayer();  
  }
}

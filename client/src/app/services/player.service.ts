import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Player } from '../models/player.model';
import { Observable, delay, map, take } from 'rxjs';
import { RegisterPlayerService } from './register-player.service';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  http = inject(HttpClient);

  private readonly baseApiUrl = environment.apiUrl + 'player/';

  getAllPlayers(): Observable<Player[] | null>{
    return this.http.get<Player[]>(this.baseApiUrl).pipe(
      map((players: Player[]) => {
        if (players)
          return players;

          return null
      })
    )
  }

  getPlayerById(): Observable<Player | null> {
    return this.http.get<Player>(this.baseApiUrl +'get-by-id/10923849128437912').pipe(
      map((player: Player |null) =>{
        if (player)
          return player;

          return null;
      })
    )
  }
}

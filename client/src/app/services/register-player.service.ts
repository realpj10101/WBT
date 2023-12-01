import { Injectable, PLATFORM_ID, inject } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Player } from '../models/player.model';
import { RegisterPlayer } from '../models/register-player.model';
import { LoginPlayer } from '../models/login-player.model';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class RegisterPlayerService {
  http = inject(HttpClient);
  router = inject(Router);
  platformId = inject(PLATFORM_ID);

  private readonly baseApiUrl = environment.apiUrl + 'registerplayer/';

  private currentPlayerSource = new BehaviorSubject<Player | null>(null);
  currentPlayer$ = this.currentPlayerSource.asObservable();

  registerPlayer(playerInput: RegisterPlayer): Observable<Player | null> {
    return this.http.post<Player>(this.baseApiUrl + 'register', playerInput).pipe(
      map(playerResponse =>{
        if (playerResponse) {
          this.setCurrentPlayer(playerResponse);

          return playerResponse;
        }

        return null
      })
    );
  }

  loginPlayer(playerInput: LoginPlayer): Observable<Player | null> {
    return this.http.post<Player>(this.baseApiUrl + 'login', playerInput).pipe(
      map(playerResponse =>{
        if (playerResponse) {
          this.setCurrentPlayer(playerResponse);

          return playerResponse;
        }

        return null;
      })
    )
  }

  setCurrentPlayer(player: Player): void {
    this.currentPlayerSource.next(player);

    if (isPlatformBrowser(this.platformId))
      localStorage.setItem('player', JSON.stringify(player));

    this.router.navigateByUrl('');
  }

  logOutPlayer(): void {
    this.currentPlayerSource.next(null);

    if (isPlatformBrowser(this.platformId))
      localStorage.removeItem('player');

    this.router.navigateByUrl('registerplayer/login')
  }
}

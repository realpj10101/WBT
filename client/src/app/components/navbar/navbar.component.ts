import { Component, OnInit, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Player } from '../../models/player.model';
import { RegisterPlayerService } from '../../services/register-player.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, MatToolbarModule, MatButtonModule, MatIconModule,
    MatMenuModule, MatDividerModule, MatListModule, HttpClientModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  registerPlayerService = inject(RegisterPlayerService);

  player$: Observable<Player | null> | undefined;


  ngOnInit(): void {
    this.player$ = this.registerPlayerService.currentPlayer$;
  }

  logOut(): void {
    this.registerPlayerService.logOutPlayer();
  }
}

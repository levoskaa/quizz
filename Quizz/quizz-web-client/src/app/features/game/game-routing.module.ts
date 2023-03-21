import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GameDetailPageComponent } from './pages/game-detail-page/game-detail-page.component';
import { GameLobbyPageComponent } from './pages/game-lobby-page/game-lobby-page.component';
import { GamesPageComponent } from './pages/games-page/games-page.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: GamesPageComponent,
  },
  {
    path: ':id',
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: GameDetailPageComponent,
      },
      {
        path: 'lobby',
        component: GameLobbyPageComponent,
      },
    ],
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GameRoutingModule {}

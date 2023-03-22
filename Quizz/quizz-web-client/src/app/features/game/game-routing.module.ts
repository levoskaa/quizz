import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GameDetailPageComponent } from './pages/game-detail-page/game-detail-page.component';
import { GamesPageComponent } from './pages/games-page/games-page.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: GamesPageComponent,
  },
  {
    path: ':id',
    component: GameDetailPageComponent,
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

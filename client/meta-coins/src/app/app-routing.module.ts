import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { WalletsListComponent } from './components/wallets-list/wallets-list.component';
import { CoinsListComponent } from './components/coins-list/coins-list.component';

const routes: Routes = [
  {path: " ", redirectTo: "/login", pathMatch: 'full'},
  {path: "login", component: UserLoginComponent},
  {path: "my-wallets", component: WalletsListComponent},
  {path: "my-coins", component: CoinsListComponent},
//  {path: "my-transactions", component: WalletsListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

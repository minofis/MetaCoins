import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SendCoinComponent } from './components/transaction/send-coin/send-coin.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { WalletComponent } from './components/wallet/wallet/wallet.component';
import { WalletCoinsComponent } from './components/wallet/wallet-coins/wallet-coins.component';
import { CreateCoinComponent } from './components/coin/create-coin/create-coin.component';
import { OwnerRecordsComponent } from './components/coin/owner-records/owner-records.component';
import { CoinComponent } from './components/coin/coin/coin.component';
import { FeedComponent } from './components/pages/feed/feed.component';
import { ProfileComponent } from './components/pages/profile/profile.component';
import { NotFoundComponent } from './components/pages/not-found/not-found.component';
import { UserLikesComponent } from './components/pages/user-likes/user-likes.component';

const routes: Routes = [
  {path: "", redirectTo: "/register", pathMatch: 'full'},
  {path: "login", component: LoginComponent},
  {path: "register", component: RegisterComponent},

  {path: "feed", component: FeedComponent},
  {path: ":username/profile", component: ProfileComponent},
  {path: ":username/likes", component: UserLikesComponent},
  

//  {path: "@:username", component: UserProfileComponent},

  {path: ":username/wallet", component: WalletComponent},
  {path: ":username/wallet/coins", component: WalletCoinsComponent},

  {path: ":username/wallet/coins/create", component: CreateCoinComponent},
  {path: ":username/wallet/coins/:id", component: CoinComponent},
  {path: ":username/wallet/coins/:id/send", component: SendCoinComponent},
  {path: ":username/wallet/coins/:id/owner-records", component: OwnerRecordsComponent},
  {path: "**", component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

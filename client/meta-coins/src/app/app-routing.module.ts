import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SendCoinComponent } from './components/transactions/send-coin/send-coin.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { WalletComponent } from './components/wallet/wallet/wallet.component';
import { WalletCoinsComponent } from './components/wallet/wallet-coins/wallet-coins.component';
import { CreateCoinComponent } from './components/coin/create-coin/create-coin.component';
import { OwnerRecordsComponent } from './components/coin/owner-records/owner-records.component';
import { CoinComponent } from './components/coin/coin/coin.component';

const routes: Routes = [
  {path: "", redirectTo: "/register", pathMatch: 'full'},
  {path: "login", component: LoginComponent},
  {path: "register", component: RegisterComponent},

//  {path: "@:username", component: UserProfileComponent},

  {path: ":username/wallet", component: WalletComponent},
  {path: ":username/wallet/coins", component: WalletCoinsComponent},

  {path: ":username/wallet/coins/create", component: CreateCoinComponent},
  {path: ":username/wallet/coins/:id", component: CoinComponent},
  {path: ":username/wallet/coins/:id/send", component: SendCoinComponent},
  {path: ":username/wallet/coins/:id/owner-records", component: OwnerRecordsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

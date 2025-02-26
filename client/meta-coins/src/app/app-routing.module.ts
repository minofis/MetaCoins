import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { WalletCoinsComponent } from './components/wallets/wallet-coins/wallet-coins.component';
import { WalletComponent } from './components/wallets/wallet/wallet.component';
import { WalletsComponent } from './components/wallets/wallets/wallets.component';
import { CoinOwnerRecordsComponent } from './components/coins/coin-owner-records/coin-owner-records.component';
import { CoinComponent } from './components/coins/coin/coin.component';
import { CoinCreateComponent } from './components/coins/coin-create/coin-create.component';
import { SendCoinComponent } from './components/transactions/send-coin/send-coin.component';

const routes: Routes = [
  {path: " ", redirectTo: "/login", pathMatch: 'full'},
  {path: "login", component: UserLoginComponent},
  {path: "my-wallets", component: WalletsComponent},
  {path: "wallets/:id/coins", component: WalletCoinsComponent},
  {path: "coins/:id/owner-records", component: CoinOwnerRecordsComponent},
  {path: "wallets/:id", component: WalletComponent},
  {path: "coins/:id", component: CoinComponent},
  {path: "coins/create/:id", component: CoinCreateComponent},
  {path: "transactions/transfer-coin", component: SendCoinComponent},
//  {path: "my-transactions", component: WalletsListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { TransactionCreateRequest } from '../../../models/transaction-create-request';
import { Router } from '@angular/router';
import { TransactionsService } from '../../../services/transactions.service';

@Component({
  selector: 'app-send-coin',
  templateUrl: './send-coin.component.html',
  styleUrl: './send-coin.component.scss'
})
export class SendCoinComponent {

  coinId!: string;
  walletId!: string;

  response!: string

  public sendCoinForm = new FormGroup({
    recipientWalletId: new FormControl(''),
  });

  constructor(private router: Router, private _transactionsService: TransactionsService)
  {
    const navigation = this.router.getCurrentNavigation();
    
    if (navigation?.extras.state) {
      this.coinId = navigation.extras.state['coinId'];
      this.walletId = navigation.extras.state['walletId'];
    }
  }

  public onSend(): void
  {    
    const transactionData: TransactionCreateRequest =
    {
      recipientWalletId: this.sendCoinForm.value.recipientWalletId ?? '',
      senderWalletId: this.walletId,
      coinId: this.coinId
    }
      this._transactionsService.sendCoin(transactionData).subscribe(
        (response) => {
          this.response = response
          console.log(response)
          this.router.navigate(['/wallets', this.walletId, 'coins'])
        }
      );
  }

  goBack()
  {
    this.router.navigate(['/coins', this.coinId])
  }
}

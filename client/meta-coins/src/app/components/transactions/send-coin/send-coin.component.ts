import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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
    recipientUsername: new FormControl(''),
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
    const transaction =
    {
      recipientUsername: this.sendCoinForm.value.recipientUsername ?? '',
      senderWalletId: this.walletId,
      coinId: this.coinId
    }
    /*
      this._transactionsService.sendCoin(transaction).subscribe(
        (response) => {
          this.response = response
          console.log(response)
          this.router.navigate(['/wallets', this.walletId, 'coins'])
        }
      );
    */
  }

  goBack()
  {
    this.router.navigate(['/coins', this.coinId])
  }
}

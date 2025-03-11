import { Component } from '@angular/core';
import { TransactionService } from '../../../services/transaction.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-send-coin',
  templateUrl: './send-coin.component.html',
  styleUrl: './send-coin.component.scss'
})
export class SendCoinComponent {

  coinId!: string;
  senderUsername!: string;

  response!: string

  public sendCoinForm = new FormGroup({
    recipientUsername: new FormControl(''),
  });

  constructor(private router: Router, private route: ActivatedRoute, private _transactionService: TransactionService)
  {

    this.coinId = this.route.snapshot.paramMap.get('id') || '';
    this.senderUsername = this.route.snapshot.paramMap.get('username') || '';
  }

  public onSend(): void
  { 
    const transaction =
    {
      senderUsername: this.senderUsername,
      recipientUsername: this.sendCoinForm.value.recipientUsername ?? '',
      coinId: this.coinId
    }
    this._transactionService.sendCoin(transaction).subscribe(
        (response) => {
          this.response = response
          console.log(response)
          this.router.navigate([this.senderUsername, 'wallet', 'coins'])
        }
      );
  }

  goBack()
  {
    this.router.navigate(['/coins', this.coinId])
  }
}

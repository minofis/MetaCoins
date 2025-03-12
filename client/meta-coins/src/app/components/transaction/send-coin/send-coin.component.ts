import { Component } from '@angular/core';
import { TransactionService } from '../../../services/transaction.service';
import { Location } from '@angular/common';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-send-coin',
  templateUrl: './send-coin.component.html',
  styleUrl: './send-coin.component.scss'
})
export class SendCoinComponent {

  coinId!: string;
  senderUsername!: string;

  errorMessage!: string

  public sendCoinForm = new FormGroup({
    recipientUsername: new FormControl(''),
  });

  constructor(private router: Router, private route: ActivatedRoute, private _transactionService: TransactionService, private location: Location)
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
    this._transactionService.sendCoin(transaction).subscribe({
        next: (response) => {
          console.log(response)
          this.router.navigate([this.senderUsername, 'wallet', 'coins'])
        },
        error: (error: HttpErrorResponse) => {
          let errorMessage = 'An error occurred.';
    
          if (error.message) {
            errorMessage = error.message;
          }
          this.errorMessage = errorMessage
        }
  });
  }

  goBack()
  {
    this.location.back();
  }
}

import { Component } from '@angular/core';
import { FinancialFormService } from '../../services/financial-form.service';
import { FinancialStatusHistory } from '../../shared/models/plan.model';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent {
  private userId!: string | null;
  private routeSub!: Subscription;
  public financialStatusHistory!: FinancialStatusHistory[];

  ngOnInit(): void {
    this.userId = sessionStorage.getItem("userId");
    this.loadFinancialStatusHistory();
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  constructor(private financialFormService: FinancialFormService, private router: Router) {

  }

  private loadFinancialStatusHistory(): void {
    if (this.userId) {
      this.financialFormService.getFinancialStatusHistory(this.userId).subscribe(
        (result: FinancialStatusHistory[]) => {
          this.financialStatusHistory = result;
        },
        (error) => {
          console.error('Error fetching financial status history', error);
        }
      )
    }
  }

  goToDetails(historyId: string) {
    this.router.navigate(['/plan', historyId]);
  }
}

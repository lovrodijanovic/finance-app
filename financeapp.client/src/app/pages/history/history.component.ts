import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { FinancialFormService } from '../../services/financial-form.service';
import { FinancialStatusHistory } from '../../shared/models/plan.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit, OnDestroy {
  private routeSub!: Subscription;
  public financialStatusHistory: FinancialStatusHistory[] = [];
  public selectedFinancialStatusId: string | null = null;

  constructor(
    private financialFormService: FinancialFormService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.loadFinancialStatusHistory();
    this.route.params.subscribe(params => {
      this.selectedFinancialStatusId = params['financialStatusId'] || null;
    });
  }

  ngOnDestroy(): void {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
  }

  private loadFinancialStatusHistory(): void {
    const userId = sessionStorage.getItem('userId');
    if (userId) {
      this.financialFormService.getFinancialStatusHistory(userId).subscribe(
        (result: FinancialStatusHistory[]) => {
          this.financialStatusHistory = result;
        },
        (error) => {
          console.error('Error fetching financial status history', error);
        }
      );
    }
  }

  goToDetails(id: string): void {
    this.router.navigate(['/plan', { financialStatusId: id }]);
  }
}

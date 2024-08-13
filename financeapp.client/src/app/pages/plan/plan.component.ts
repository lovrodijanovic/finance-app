import { Component, OnInit } from '@angular/core';
import { FinancialFormService } from '../../services/financial-form.service';
import { FormResult } from '../../shared/models/plan.model';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-plan',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './plan.component.html',
  styleUrl: './plan.component.css'
})

export class PlanComponent implements OnInit {
  public financialStatusId!: string;
  private routeSub!: Subscription;
  sectionCounter: number = 0;

  public financialFormResults: FormResult = {
    financialScore: 0,
    hasBudget: false,
    hasDebt: false,
    hasDebtsWithHighInterestRate: false,
    hasDebtsWithMediumInterestRate: false,
    hasDebtsWithLowInterestRate: false,
    hasEmergencyFund: false,
    hasFullEmergencyFund: false,
    hasSmallEmergencyFund: false,
    hasInvestments: false,
    hasVoluntaryPensionInsurance: false,
    isFullVoluntaryPensionInsuranceContribution: false
  }

  constructor(public financialFormService: FinancialFormService, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      this.financialStatusId = params['financialStatusId'];
    })
    this.loadFinancialResults();
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  private loadFinancialResults(): void {
    if (this.financialStatusId) {
      this.financialFormService.getResults(this.financialStatusId).subscribe(
        (result: FormResult) => {
          this.financialFormResults = result;
        },
        (error) => {
          console.error('Error fetching financial results', error);
        }
      );
    }
  }
}

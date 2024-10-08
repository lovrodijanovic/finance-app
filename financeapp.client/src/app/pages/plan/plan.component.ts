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
  private financialStatusId!: string;
  private routeSub!: Subscription;

  public financialFormResults: FormResult = {
    financialScore: 0,
    hasBudget: false,
    hasDebt: false,
    hasDebtsWithHighInterestRate: false,
    highInterestRateDebtPayoffs: [],
    hasDebtsWithMediumInterestRate: false,
    mediumInterestRateDebtPayoffs: [],
    hasDebtsWithLowInterestRate: false,
    lowInterestRateDebtPayoffs: [],
    hasEmergencyFund: false,
    hasFullEmergencyFund: false,
    hasSmallEmergencyFund: false,
    hasInvestments: false,
    hasVoluntaryPensionInsurance: false,
    isFullVoluntaryPensionInsuranceContribution: false,
    investmentAmountSuggestion: 0,
    equityPercentageSuggestion: 0,
    hasLowInvestments: false
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

  public loadFinancialResults(): void {
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

  monthsToYears(numberOfPayments: number): string {
    const years = Math.floor(numberOfPayments / 12);
    const months = numberOfPayments % 12;

    if (numberOfPayments <= 12) {
      return ``;
    }
    else
      return `${years} godina${years > 0 && months > 0 ? ' i ' : ''}${months > 0 ? months + ' mjeseci' : ''}`;
  }
}

import { Component } from '@angular/core';
import { SharedModule } from '../../shared/shared/shared.module';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { InvestmentType } from '../../shared/models/plan.model';
import { FinancialFormService } from '../../services/financial-form.service';

@Component({
  selector: 'app-create-plan',
  standalone: true,
  imports: [SharedModule, FormsModule, ReactiveFormsModule],
  templateUrl: './create-plan.component.html',
  styleUrls: ['./create-plan.component.css'],
})
export class CreatePlanComponent {
  public investmentTypes: InvestmentType[] = [];

  formGroup: FormGroup;

  constructor(
    private financialFormService: FinancialFormService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.formGroup = this.formBuilder.group({
      userId: [null],

      hasBudget: [null, Validators.required],

      hasEmergencyFund: [null],
      emergencyFund: this.formBuilder.group({
        amount: [null, Validators.required],
        monthlyContribution: [null, Validators.required],
      }),

      hasDebt: [null, Validators.required],
      debts: this.formBuilder.array([]),

      hasVoluntaryPensionInsurance: [null, Validators.required],
      voluntaryPensionInsurance: this.formBuilder.group({
        amount: [null, Validators.required],
        monthlyContribution: [null, Validators.required],
      }),

      hasInvestments: [null, Validators.required],
      investments: this.formBuilder.array([]),

      riskSensitivity: [null, [Validators.required, Validators.min(1), Validators.max(5)]],

      netEarnings: [null, Validators.required],
    });

    this.formGroup.get('hasDebt')?.valueChanges.subscribe(value => {
      if (value === 'false') {
        this.clearDebts();
      }
    });

    this.formGroup.get('hasInvestments')?.valueChanges.subscribe(value => {
      if (value === 'false') {
        this.clearInvestments();
      }
    });

    this.formGroup.get('hasVoluntaryPensionInsurance')?.valueChanges.subscribe(value => {
      this.toggleVoluntaryPensionInsurance(value);
    });

    this.formGroup.get('hasEmergencyFund')?.valueChanges.subscribe(value => {
      this.toggleEmergencyFund(value);
    });

    this.financialFormService.getInvestmentTypes().subscribe(
      result => {
        this.investmentTypes = result;
      },
      error => console.error(error)
    );
  }

  get debts(): FormArray {
    return this.formGroup.get('debts') as FormArray;
  }

  get investments(): FormArray {
    return this.formGroup.get('investments') as FormArray;
  }

  get voluntaryPensionInsurance(): FormGroup {
    return this.formGroup.get('voluntaryPensionInsurance') as FormGroup;
  }

  get emergencyFund(): FormGroup {
    return this.formGroup.get('emergencyFund') as FormGroup;
  }

  private toggleEmergencyFund(hasEmergencyFund: string | boolean) {
    const enabled = hasEmergencyFund === 'true' || hasEmergencyFund === true;
    if (enabled) {
      this.emergencyFund.enable();
    } else {
      this.emergencyFund.disable();
      this.emergencyFund.reset();
    }
  }

  private toggleVoluntaryPensionInsurance(hasInsurance: string | boolean) {
    const enabled = hasInsurance === 'true' || hasInsurance === true;
    if (enabled) {
      this.voluntaryPensionInsurance.enable();
    } else {
      this.voluntaryPensionInsurance.disable();
      this.voluntaryPensionInsurance.reset();
    }
  }

  addDebt() {
    this.debts.push(
      this.formBuilder.group({
        debtName: [null],
        interestRate: [null],
        monthlyContribution: [null],
        remainingBalance: [null]
      })
    );
  }

  addInvestment() {
    this.investments.push(
      this.formBuilder.group({
        investmentType: [this.investmentTypes[0]?.id || ''],
        amount: [null],
        monthlyContribution: [null],
      })
    );
  }

  deleteDebt(num: number) {
    this.debts.removeAt(num);
  }

  deleteInvestment(num: number) {
    this.investments.removeAt(num);
  }

  clearDebts() {
    while (this.debts.length) {
      this.debts.removeAt(0);
    }
  }

  clearInvestments() {
    while (this.investments.length) {
      this.investments.removeAt(0);
    }
  }

  onSubmit() {
    const financialForm = {
      ...this.formGroup.value,
      userId: '123e4567-e89b-12d3-a456-426614174000',
      hasBudget: this.convertToBoolean(this.formGroup.value.hasBudget),
      hasDebt: this.convertToBoolean(this.formGroup.value.hasDebt),
      hasEmergencyFund: this.convertToBoolean(this.formGroup.value.hasEmergencyFund),
      hasInvestments: this.convertToBoolean(this.formGroup.value.hasInvestments),
      hasVoluntaryPensionInsurance: this.convertToBoolean(this.formGroup.value.hasVoluntaryPensionInsurance),
    };

    this.financialFormService.submitForm(financialForm).subscribe(
      response => {
        const financialStatusId = response;
        this.router.navigate(['/plan', financialStatusId]);
      },
      error => {
        console.error('Error submitting form', error);
      }
    );
  }

  private convertToBoolean(value: any): boolean {
    return value === true || value === 'true';
  }
}

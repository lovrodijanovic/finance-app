import { Component } from '@angular/core';
import { SharedModule } from '../../shared/shared/shared.module';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FinancialInformation } from '../../shared/models/plan.model';

@Component({
  selector: 'app-create-plan',
  standalone: true,
  imports: [SharedModule, FormsModule, ReactiveFormsModule],
  templateUrl: './create-plan.component.html',
  styleUrl: './create-plan.component.css'
})
export class CreatePlanComponent {

  form: FormGroup;

  debts!: FormArray;
  investments!: FormArray;

  constructor(private formBuilder: FormBuilder, private router: Router) {
    this.form = this.formBuilder.group({
      userId: ['', Validators.required],

      hasBudget: ['', Validators.required],

      hasEmergencyFund: ['', Validators.required],
      emergencyFund: this.formBuilder.group({
        amount: [''],
        monthlyContribution: ['']
      }),

      hasDebt: [''],
      debts: this.formBuilder.array([]),

      hasVoluntaryPensionInsurance: ['', Validators.required],
      voluntaryPensionInsurance: this.formBuilder.group({
        amount: [''],
        monthlyContribution: ['']
      }),

      hasInvestments: [''],
      investments: this.formBuilder.array([]),

      riskSensitivity: ['', [Validators.required, Validators.min(1), Validators.max(5)]],

      netEarnings: ['', Validators.required],
    });

    this.addDebt();
    this.addInvestment();


    this.debts = this.form.get('debts') as FormArray;
    this.form.get('debts')?.valueChanges.subscribe(value => {
      this.debts = this.form.get('debts') as FormArray;
    });

    this.investments = this.form.get('investments') as FormArray;
    this.form.get('investments')?.valueChanges.subscribe(value => {
      this.investments = this.form.get('investments') as FormArray;
    });
  }

  addDebt() {
    const debts = this.form.get('debts') as FormArray;
    debts.push(this.formBuilder.group({
      type: [''],
      amount: [''],
      interestRate: ['']
    }));
  }

  addInvestment() {
    const investments = this.form.get('investments') as FormArray;
    investments.push(this.formBuilder.group({
      investmentType: [''],
      amount: [''],
      monthlyContribution: [''],
    }));
  }

  deleteDebt(num: number) {
    this.debts.removeAt(num);
  }

  deleteInvestment(num: number) {
    this.investments.removeAt(num);
  }

  onSubmit() {
    const model = new FinancialInformation(this.form.getRawValue());
    console.log(model);

    
    this.router.navigate(['/plan']);
  }
}

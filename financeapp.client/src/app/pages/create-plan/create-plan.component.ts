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

  creditsAndDebts!: FormArray;
  otherFunds!: FormArray;

  constructor(private formBuilder: FormBuilder, private router: Router) {
    this.form = this.formBuilder.group({
      budget: ['', Validators.required],
      emergencyFund: ['', Validators.required],
      emergencyFundAmount: [''],
      hasCreditsAndDebts: [''],
      creditsAndDebts: this.formBuilder.array([]),
      pensionFund: ['', Validators.required],
      pensionFundAmount: [''],
      hasOtherFunds: [''],
      otherFunds: this.formBuilder.array([]),
      age: ['', [Validators.required]],
      riskTolerance: ['', [Validators.required, Validators.min(1), Validators.max(5)]],
      netMonthlyIncome: ['', Validators.required],
    });

    this.addCredit();
    this.addOtherFunds();


    this.creditsAndDebts = this.form.get('creditsAndDebts') as FormArray;
    this.form.get('creditsAndDebts')?.valueChanges.subscribe(value => {
      this.creditsAndDebts = this.form.get('creditsAndDebts') as FormArray;
    });

    this.otherFunds = this.form.get('otherFunds') as FormArray;
    this.form.get('otherFunds')?.valueChanges.subscribe(value => {
      this.otherFunds = this.form.get('otherFunds') as FormArray;
    });
  }

  addCredit() {
    const creditsAndDebts = this.form.get('creditsAndDebts') as FormArray;
    creditsAndDebts.push(this.formBuilder.group({
      type: [''],
      amount: [''],
      interestRate: ['']
    }));
  }

  addOtherFunds() {
    const otherFunds = this.form.get('otherFunds') as FormArray;
    otherFunds.push(this.formBuilder.group({
      otherFundType: [''],
      otherFundTotalAmount: [''],
      otherFundMonthAmount: [''],
    }));
  }

  deleteDebt(num: number) {
    this.creditsAndDebts.removeAt(num);
  }

  deleteOtherFund(num: number) {
    this.otherFunds.removeAt(num);
  }

  onSubmit() {
    const model = new FinancialInformation(this.form.getRawValue());
    console.log(model);

    
    this.router.navigate(['/plan']);
  }

}

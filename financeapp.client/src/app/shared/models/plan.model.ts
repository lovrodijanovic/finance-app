export class Debt {
  debtName: string;
  interestRate: number;
  remainingBalance: number;
  monthlyContribution: number;
  constructor(
    debtName: string = '',
    remainingBalance: number = 0,
    interestRate: number = 0,
    monthlyContribution: number = 0
  ) {
    this.debtName = debtName;
    this.remainingBalance = remainingBalance;
    this.interestRate = interestRate;
    this.monthlyContribution = monthlyContribution;
  }
}

export class Investment {
  investmentType: string;
  amount: number;
  monthlyContribution: number;

  constructor(
    investmentType: string = '',
    amount: number = 0,
    monthlyContribution: number = 0
  ) {
    this.investmentType = investmentType;
    this.amount = amount;
    this.monthlyContribution = monthlyContribution;
  }
}

export class InvestmentType {
  id: string;
  name: string;
  description: string;
  abbreviation: string;
  croatianName: string;
  croatianDescription: string;
  constructor(
    id: string,
    name: string,
    description: string,
    abbreviation: string,
    croatianName: string,
    croatianDescription: string
  ) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.abbreviation = abbreviation;
    this.croatianName = croatianName;
    this.croatianDescription = croatianDescription
  }
}

export class EmergencyFund {
  amount: number;
  monthlyContribution: number;
  constructor(amount: number = 0, monthlyContribution: number = 0) {
    this.amount = amount;
    this.monthlyContribution = monthlyContribution;
  }
}

export class VoluntaryPensionInsurance {
  amount: number;
  monthlyContribution: number;
  constructor(amount: number = 0, monthlyContribution: number = 0) {
    this.amount = amount;
    this.monthlyContribution = monthlyContribution;
  }
}

export class FinancialForm {
  hasBudget: boolean;
  hasEmergencyFund: boolean;
  emergencyFund: EmergencyFund;
  hasDebt: boolean;
  debts: Debt[];
  hasVoluntaryPensionInsurance: boolean;
  voluntaryPensionInsurance: VoluntaryPensionInsurance;
  hasInvestments: boolean;
  investments: Investment[];
  riskSensitivity: number;
  netEarnings: number;

  constructor(model: any) {
    this.hasBudget = model.hasBudget;
    this.hasEmergencyFund = model.hasEmergencyFund;
    this.emergencyFund = model.emergencyFund;
    this.hasDebt = model.hasDebt;
    this.debts = model.debts;
    this.hasVoluntaryPensionInsurance = model.hasVoluntaryPensionInsurance;
    this.voluntaryPensionInsurance = model.voluntaryPensionInsurance;
    this.hasInvestments = model.hasInvestments;
    this.investments = model.investments;
    this.riskSensitivity = model.riskSensitivity;
    this.netEarnings = model.netEarnings;
  }
}

export class FormResult {
  financialScore: number;
  hasBudget: boolean;
  hasEmergencyFund: boolean;
  hasSmallEmergencyFund: boolean;
  hasFullEmergencyFund: boolean;
  hasDebt: boolean;
  hasDebtsWithHighInterestRate: boolean;
  highInterestRateDebtPayoffs: DebtPayoff[];
  hasDebtsWithMediumInterestRate: boolean;
  mediumInterestRateDebtPayoffs: DebtPayoff[];
  hasDebtsWithLowInterestRate: boolean;
  lowInterestRateDebtPayoffs: DebtPayoff[];
  hasVoluntaryPensionInsurance: boolean;
  isFullVoluntaryPensionInsuranceContribution: boolean;
  hasInvestments: boolean;

  constructor(model: any) {
    this.financialScore = model.financialScore;
    this.hasBudget = model.hasBudget;
    this.hasEmergencyFund = model.hasEmergencyFund;
    this.hasSmallEmergencyFund = model.hasSmallEmergencyFund;
    this.hasFullEmergencyFund = model.hasFullEmergencyFund;
    this.hasDebt = model.hasDebt;
    this.hasDebtsWithHighInterestRate = model.hasDebtsWithHighInterestRate;
    this.highInterestRateDebtPayoffs = model.highInterestRateDebtPayoffs;
    this.hasDebtsWithMediumInterestRate = model.hasDebtsWithMediumInterestRate;
    this.mediumInterestRateDebtPayoffs = model.mediumInterestRateDebtPayoffs;
    this.hasDebtsWithLowInterestRate = model.hasDebtsWithLowInterestRate;
    this.lowInterestRateDebtPayoffs = model.lowInterestRateDebtPayoffs;
    this.hasVoluntaryPensionInsurance = model.hasVoluntaryPensionInsurance;
    this.isFullVoluntaryPensionInsuranceContribution = model.isFullVoluntaryPensionInsuranceContribution;
    this.hasInvestments = model.hasInvestments;
  }
}

export interface DebtPayoff {
  debtName: string;
  interestRate: number;
  numberOfPayments: number;
  totalInterest: number;
  totalPayments: number;
}

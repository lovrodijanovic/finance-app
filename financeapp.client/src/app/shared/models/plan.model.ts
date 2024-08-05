export class Debt {
    type: string;
    principal: number;
    interestRate: number;
    maturityYears: number;
    monthlyContribution: number;
    constructor(type: string = '', principal: number = 0, interestRate: number = 0, maturityYears: number = 0, monthlyContribution: number = 0) {
        this.type = type;
        this.principal = principal;
        this.interestRate = interestRate;
        this.maturityYears = maturityYears;
        this.monthlyContribution = monthlyContribution;
    }
}

export class Investment {
    investmentType: string;
    amount: number;
    monthlyContribution: number;

    constructor(investmentType: string = '', amount: number = 0, monthlyContribution: number = 0) {
      this.investmentType = investmentType;
      this.amount = amount;
      this.monthlyContribution = monthlyContribution;
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

export class FinancialInformation {
    hasBudget: string;
    hasEmergencyFund: string;
    emergencyFund: EmergencyFund;
    hasDebt: boolean;
    debts: Debt[];
    hasVoluntaryPensionInsurance: string;
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

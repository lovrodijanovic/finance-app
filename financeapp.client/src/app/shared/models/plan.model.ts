export class CreditOrDebt {
    type: string;
    amount: number;
    interestRate: number;

    constructor(type: string = '', amount: number = 0, interestRate: number = 0) {
        this.type = type;
        this.amount = amount;
        this.interestRate = interestRate;
    }
}

export class OtherFund {
    otherFundType: string;
    otherFundTotalAmount: number;
    otherFundMonthAmount: number;

    constructor(otherFundType: string = '', otherFundTotalAmount: number = 0, otherFundMonthAmount: number = 0) {
        this.otherFundType = otherFundType;
        this.otherFundTotalAmount = otherFundTotalAmount;
        this.otherFundMonthAmount = otherFundMonthAmount;
    }
}

export class FinancialInformation {
    budget: string;
    emergencyFund: string;
    emergencyFundAmount: number;
    hasCreditsAndDebts: boolean;
    creditsAndDebts: CreditOrDebt[];
    pensionFund: string;
    pensionFundAmount: number;
    hasOtherFunds: boolean;
    otherFunds: OtherFund[];
    age: number;
    riskTolerance: number;
    netMonthlyIncome: number;

    constructor(model: any) {
        this.budget = model.budget;
        this.emergencyFund = model.emergencyFund;
        this.emergencyFundAmount = model.emergencyFundAmount;
        this.hasCreditsAndDebts = model.hasCreditsAndDebts;
        this.creditsAndDebts = model.creditsAndDebts;
        this.pensionFund = model.pensionFund;
        this.pensionFundAmount = model.pensionFundAmount;
        this.hasOtherFunds = model.hasOtherFunds;
        this.otherFunds = model.otherFunds;
        this.age = model.age;
        this.riskTolerance = model.riskTolerance;
        this.netMonthlyIncome = model.netMonthlyIncome;
    }
}
import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FinancialForm, FormResult, InvestmentType } from '../shared/models/plan.model';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class FinancialFormService {
  constructor(
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string
  ) {}

  public submitForm(formData: FinancialForm) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.http.post<FinancialForm>(
      this.baseUrl + `financialForm`,
      formData,
      httpOptions
    );
  }

  public getResults(resultId: string): Observable<FormResult> {
    return this.http.get<FormResult>(this.baseUrl + `financialForm/${resultId}`)
  }

  public getInvestmentTypes(): Observable<InvestmentType[]> {
    return this.http.get<InvestmentType[]>(this.baseUrl + `financialForm/get-investment-types`)
  }
}

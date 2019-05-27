import { Observable } from '../../../node_modules/rxjs';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ICalculationDetails } from '../models/calculation-details';
import { Coordinates } from '../models/coordinates';
import { FormGroup, FormControl } from '@angular/forms';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};
const apiEndpoint = '/api/DistanceCalculation';

@Injectable({
  providedIn: 'root',
})

export class DistanceCalculationService {
  private options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

  constructor(private http: HttpClient) { }

  //getEmpireStateToTheSpireInKm() {
  //  return this.http.get<ICalculationDetails>("/api/DistanceCalculation/EmpireStateToTheSpireInKm", this.options);
  //}

  postDistanceCoordinates(coordinates: Coordinates): Observable<ICalculationDetails> {
    return this.http.post<ICalculationDetails>("/api/DistanceCalculation/", coordinates)
  }

  getDistanceCalculation(latA: number, longA: number, latB: number, longB: number, unit: string): Observable<ICalculationDetails> {
    const calculateDistanceUrl = "/api/Distance/latA=${latA}&longA=${longA}&latB=${latB}&longB=${longB}&unit=${unit}";
    return this.http.get<ICalculationDetails>(calculateDistanceUrl, this.options);
  }
}

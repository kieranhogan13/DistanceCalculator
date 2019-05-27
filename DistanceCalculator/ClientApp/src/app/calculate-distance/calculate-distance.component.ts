import { Component, Inject} from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ICalculationDetails } from '../models/calculation-details';
import { DistanceCalculationService } from '../services/distance-calculation-service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Coordinates } from '../models/coordinates';

@Component({
  selector: 'app-calculate-distance',
  templateUrl: './calculate-distance.component.html',
  styleUrls: ['./calculate-distance.component.css'],
})
export class CalculateDistanceComponent {

  constructor(private distanceCalculationService: DistanceCalculationService) { }

  public unitTypes: string[];
  public distance: number;
  public details: string;
  public calculateError: string;

  public latitudeA: number;
  public longitudeA: number;
  public latitudeB: number;
  public longitudeB: number;
  public unitType: string;
  public coordinates: Coordinates;

  caclulateForm = new FormGroup({
    latitudeA: new FormControl('', [Validators.required, Validators.pattern('\\-?\\d*\\.?\\d{1,7}'), Validators.max(90), Validators.min(-90)]),
    longitudeA: new FormControl('', [Validators.required, Validators.pattern('\\-?\\d*\\.?\\d{1,7}'), Validators.max(180), Validators.min(-180)]),
    latitudeB: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern('\\-?\\d*\\.?\\d{1,7}'), Validators.max(90), Validators.min(-90)]),
    longitudeB: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.pattern('\\-?\\d*\\.?\\d{1,7}'), Validators.max(180), Validators.min(-180)]),
    unitType: new FormControl('', Validators.required),
  });

  ngOnInit() {
    this.unitTypes = ["kilometres", "miles", "metres"];
    this.reset();
  }

  public fromFieldValid(): boolean {
    if (this.caclulateForm.get('latitudeA').valid && this.caclulateForm.get('longitudeA').valid) {
      this.caclulateForm.get('latitudeB').enable();
      this.caclulateForm.get('longitudeB').enable();
      return true;
    } else {
      this.caclulateForm.get('latitudeB').disable();
      this.caclulateForm.get('longitudeB').disable();
      return false;
    }
  }

  public isFieldValid(fieldName: string): boolean {
    if (this.caclulateForm.get(fieldName).valid)
      return true;
    else {
      console.log(this.caclulateForm.get(fieldName).errors);
    };
  }

  public allFieldsValid(): boolean {
    if (this.caclulateForm.get('latitudeA').valid &&
      this.caclulateForm.get('longitudeA').valid &&
      this.caclulateForm.get('latitudeB').valid &&
      this.caclulateForm.get('longitudeB').valid &&
      this.caclulateForm.get('unitType').valid)
      return true;
    false;
  }


  public reset() {
    this.coordinates = new Coordinates;
    this.caclulateForm.get('latitudeA').setValue(null);
    this.caclulateForm.get('longitudeA').setValue(null);
    this.caclulateForm.get('latitudeB').setValue(null);
    this.caclulateForm.get('longitudeB').setValue(null);
    this.caclulateForm.get('unitType').setValue(null);
    this.caclulateForm.get('latitudeA').markAsUntouched();
    this.caclulateForm.get('longitudeA').markAsUntouched();
    this.caclulateForm.get('latitudeB').markAsUntouched();
    this.caclulateForm.get('longitudeB').markAsUntouched();
    this.caclulateForm.get('unitType').markAsUntouched();
    this.distance = null;
    this.details = null;
    this.calculateError = null;
    this.fromFieldValid();
  }

  public setExampleCoordinates() {
    //EmpireState, New York
    this.caclulateForm.get('latitudeA').setValue(40.748446);
    this.caclulateForm.get('longitudeA').setValue(-73.985442);
    //The Spire, Dublin
    this.caclulateForm.get('latitudeB').setValue(53.3497625);
    this.caclulateForm.get('longitudeB').setValue(-6.26027);
    //km as default
    this.caclulateForm.get('unitType').setValue('kilometres');
    this.fromFieldValid();
  }

  public submitCalculation() {
    this.distance = null;
    this.details = null;
    if (this.allFieldsValid()) {
      this.coordinates = this.caclulateForm.value;
      this.distanceCalculationService.postDistanceCoordinates(this.coordinates).subscribe(result => {
        this.distance = result.distance;
        this.details = result.details;
        console.log(result);
      }, error => {
          console.error(error)
      }
     );
    }
    else {
      this.caclulateForm.get('latitudeA').markAsTouched();
      this.caclulateForm.get('longitudeA').markAsTouched();
      this.caclulateForm.get('latitudeB').markAsTouched();
      this.caclulateForm.get('longitudeB').markAsTouched();
      this.caclulateForm.get('unitType').markAsTouched();
    }
    
  }

  coordinate_validation_messages = {
    'latitudeA': [
      { type: 'required', message: 'From latitude is required' },
      { type: 'pattern', message: 'Must be a valid number between -90 and 90 with a max of 7 decimal points' },
      { type: 'max', message: 'Max latitude is 90°' },
      { type: 'min', message: 'Min latitude is -90°' }
    ],
    'longitudeA': [
      { type: 'required', message: 'From longitude is required' },
      { type: 'pattern', message: 'Must be a valid number between -180 and 180 with a max of 7 decimal points' },
      { type: 'max', message: 'Max longitude is 180°' },
      { type: 'min', message: 'Min longitude is -180°' }
    ],
    'latitudeB': [
      { type: 'required', message: 'To latitude is required' },
      { type: 'pattern', message: 'Must be a valid number between -90 and 90 with a max of 7 decimal points' },
      { type: 'max', message: 'Max latitude is 90°' },
      { type: 'min', message: 'Min latitude is -90°' }
    ],
    'longitudeB': [
      { type: 'required', message: 'To longitude is required' },
      { type: 'pattern', message: 'Must be a valid number between -180 and 180 with a max of 7 decimal points' },
      { type: 'max', message: 'Max longitude is 180°' },
      { type: 'min', message: 'Min longitude is -180°' }
    ],
    'unitType': [
      { type: 'required', message: 'Unit type is required' },
      { type: 'minLength', message: 'Unit type is required' }
    ]
  }

}

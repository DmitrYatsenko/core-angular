import { Injectable } from '@angular/core';
import {Http} from "@angular/http";
import 'rxjs/add/operator/map';
@Injectable()
export class VehicleService {
    constructor(private http:Http){}
    create(vehicle: any){
        return this.http.post('/api/vehicles', vehicle)
            .map(res => res.json());
    }
    getVehicle(id: any){
       return this.http.get('/api/vehicles' + id).map( res => res.json() );
    }
}
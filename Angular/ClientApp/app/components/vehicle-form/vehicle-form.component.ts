import { Component, OnInit } from '@angular/core';
import {MakeService} from "../../services/make.service";
import {FeatureService} from "../../services/feature.service";
import {VehicleService} from "../../services/vehicle.service";
import {ToastyService} from "ng2-toasty";
import {bootstrapItem} from "@angular/cli/lib/ast-tools";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes:any[];
  vehicle: any = {
    features: [],
    contact: {}
  };
  models: any[];
  features: any[];
  constructor(private makeservice: MakeService, 
              private featureservice: FeatureService,
              private toastyService: ToastyService, 
              private vehicleService: VehicleService,
              private route: ActivatedRoute,
              private router: Router
  ) {
    route.params.subscribe(p => { this.vehicle.id = +p['id']});
  }

  ngOnInit() {
    this.makeservice.getMakes().subscribe(mod => {this.makes= mod;
    this.featureservice.getFeatures().subscribe(f => this.features = f );
    console.log(this.makes);});
    this.vehicleService.getVehicle(this.vehicle.id).subscribe(v => this.vehicle = v );
  }
  onMakeChange(){
   var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId );
   this.models = selectedMake ? selectedMake.models: [];
   delete this.vehicle.modelId;
   console.log("Vehicle", this.vehicle);
  }
  onFeatureToggle(featureId: any, $event: any){
    if ($event.target.checked){
      this.vehicle.features.push(featureId);
    }
    else{
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.slice(index, 1);
    }
  }
  submit(){
    this.vehicleService.create(this.vehicle).subscribe(x => console.log(x),
    err =>{ console.error("errr") });
  }

}

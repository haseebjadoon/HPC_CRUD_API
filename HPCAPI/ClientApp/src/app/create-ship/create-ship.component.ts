import { Component, Inject, OnInit } from '@angular/core';
import { Ship } from '../shared/ship.model';
import { ShipService } from '../shared/ship.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
//import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-ship',
  templateUrl: './create-ship.component.html'
})
export class CreateShipComponent implements OnInit {
  public ship: Ship;


  constructor(public service: ShipService, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(() => {
      if (window.history.state.frontEnd) {
        console.log("HOutput: ", window.history.state.frontEnd)
        this.service.formData = JSON.parse(window.history.state.frontEnd);
      }
    });
    //this.service.formData = JSON.parse(this.location.getState() as string);
    }

  onSubmit(form: NgForm) {
    if (this.service.formData.shipId) {
      this.updateShip(form);
    }
    else {
      this.insertShip(form);
    }
  }

  insertShip(form: NgForm) {
    this.service.postShip().subscribe(
      result => {
        this.resetForm(form);
        alert("Ship added successfully!")
        //this.toastr.success('Ship added successfully', 'Ship Register');
      }
    ),
      err => {
        console.error(err);
        alert("Failed to add the ship!");
      };
  }

  updateShip(form: NgForm) {
    this.service.putShip().subscribe(
      result => {
        this.resetForm(form);
        alert("Ship updated successfully!")
        //this.toastr.success('Ship added successfully', 'Ship Register');
      }
    ),
      err => {
        console.error(err);
        alert("Failed to update the ship!");
      };
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new Ship();
  }
}

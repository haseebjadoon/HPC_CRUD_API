import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Ship } from '../shared/ship.model';
import { ShipService } from '../shared/ship.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public ships: Ship[];

  constructor(private service: ShipService, private router: Router) {
    this.onGet();
  }

    onGet() {
      this.service.getShips().subscribe(result => {
        this.ships = result;
      }, error => console.error(error));
  }

  onDelete(id: number) {
    this.service.deleteShip(id).subscribe(res => {
      this.ships = this.ships.filter(ship => ship.shipId != id);
      alert("Deleted");
    }, error => console.error(error));
  }

  onEdit(ship) {
    this.router.navigate(['/create-ship'], {
      state: {
        frontEnd: JSON.stringify(ship)
      }
    });
  }
}

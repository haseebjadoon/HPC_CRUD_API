import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Ship } from './ship.model';

@Injectable({
  providedIn: 'root'
})

export class ShipService {
  
  
  formData: Ship = new Ship();
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }

  putShip() {
    return this.http.put(this.baseUrl + 'api/ships/' + this.formData.shipId, this.formData)
  }

  postShip() {
    return this.http.post(this.baseUrl + 'api/ships', this.formData)
  }

  getShips() {
    return this.http.get<Ship[]>(this.baseUrl + 'api/ships');
  }

  deleteShip(id: number) {
    return this.http.delete(this.baseUrl + 'api/ships/' + id);
  }
}

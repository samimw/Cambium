import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { CSVRecord } from "./csv.model";
import { RoverResponse } from "./rover.response.model";

@Injectable({
  providedIn: 'root'
})
export class RoverService {


  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  postRovers(csvModel: CSVRecord[]) {
    return this.http.post<RoverResponse[]>(this.baseUrl + 'rover', csvModel);
  }
}

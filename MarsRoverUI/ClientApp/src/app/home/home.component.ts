import { Component, ViewChild } from '@angular/core';
import { CSVRecord } from '../shared/csv.model';
import { RoverResponse } from '../shared/rover.response.model';
import { RoverService } from '../shared/rover.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  records: CSVRecord[] = [];
  roverResponse: RoverResponse[];
  @ViewChild('csvReader', { static: false }) csvReader: any;

  constructor(private service: RoverService) {

  }

  uploadListener($event: any): void {

    let files = $event.srcElement.files;

    if (this.isValidCSVFile(files[0])) {

      let input = $event.target;
      let reader = new FileReader();
      reader.readAsText(input.files[0]);

      reader.onload = () => {
        let csvData = reader.result;
        let csvRecordsArray = (<string>csvData).split(/\r|\n|\n/);

        this.records = this.getDataRecordsArrayFromCSVFile(csvRecordsArray);

        this.submitRovers();

        this.fileReset();
      };

      reader.onerror = function () {
        console.log('Cannot read file!');
      };

    } else {
      alert("Invalid .csv file.");
      this.fileReset();
    }
  }

  getDataRecordsArrayFromCSVFile(csvRecordsArray: any) {
    let csvArr = [];

    for (let i = 0; i < csvRecordsArray.length; i++) {
      if (csvRecordsArray[i] != '') {
        let curruntRecord = (<string>csvRecordsArray[i]).split('|');
        let csvRecord: CSVRecord = new CSVRecord();
        csvRecord.Name = curruntRecord[0].trim();
        csvRecord.Instructions = curruntRecord[1].trim();
        csvArr.push(csvRecord);
      }
    }
    return csvArr;
  }

  isValidCSVFile(file: any) {
    return file.name.endsWith(".csv");
  }

  fileReset() {
    this.csvReader.nativeElement.value = "";
    this.records = [];
  }

  submitRovers() {
    this.service.postRovers(this.records).subscribe(
      res => {
        console.log(res);
        this.roverResponse = res;
      },
      err => {
        console.log(err);
      });
  }
} 

import { ChangeDetectorRef, Component } from '@angular/core';
import { LoaderService } from '../shared/loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css']
})
export class LoaderComponent {

  showLoader = false;

  constructor(private loaderService: LoaderService, private cdRef: ChangeDetectorRef) {  }

  ngOnInit() {
    this.init();
  }

  init() {
    this.loaderService.getSpinnerObserver().subscribe((status) => {
      this.showLoader = status === 'start';
      this.cdRef.detectChanges();
    });
  }
}

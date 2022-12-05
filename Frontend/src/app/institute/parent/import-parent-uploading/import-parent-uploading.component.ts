import { ChangeDetectorRef, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-import-parent-uploading',
  templateUrl: './import-parent-uploading.component.html',
  styleUrls: ['./import-parent-uploading.component.css']
})
export class ImportParentUploadingComponent implements OnInit, OnChanges {
  @Input() progress$: Subject<number> = new Subject<number>();
  progress = 0;
  constructor(private cdref: ChangeDetectorRef) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.cdref.detectChanges();
  }

   ngOnInit(): void {
    this.progress$.subscribe(response => {
      if (response) {
        this.progress = response;
      }
    });
  }
}

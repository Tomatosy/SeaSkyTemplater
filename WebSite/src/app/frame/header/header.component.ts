import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonService } from 'src/app/shared/services/common.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  num = 0;
  constructor(private commonService: CommonService) {
  }

  ngOnInit() {

  }

  ngOnDestroy() {
  }
}

import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PageModel } from '../../model/page.model';

@Component({
  selector: 'app-pagination-box',
  templateUrl: './pagination-box.component.html',
  styleUrls: ['./pagination-box.component.scss']
})
export class PaginationBoxComponent implements OnInit {
  @Input() title: string;
  @Input() pageInfo: PageModel<any> = new PageModel<any>();
  @Input() isShowtitle = true;
  @Input() isShowSelectPageSize = true;
  @Input() isShowGoTo = true;
  @Output() pageChange: EventEmitter<PageModel<any>> = new EventEmitter<PageModel<any>>();

  constructor() { }

  ngOnInit() {
  }

  onChangePageSize(pageSize: number) {
    this.pageInfo.pageSize = pageSize;
    this.pageChange.emit(this.pageInfo);
  }
  onPageChange(currentPage: number) {
    this.pageInfo.pageNO = currentPage;
    this.pageChange.emit(this.pageInfo);
  }
}

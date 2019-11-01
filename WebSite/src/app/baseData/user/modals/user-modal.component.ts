import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UserInputModel } from 'src/app/shared/model/User.model';
import { CommonService } from './../../../shared/services/common.service';
import { BaseDataService } from 'src/app/shared/services/base-data.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user-modal',
  templateUrl: './user-modal.component.html',
  styleUrls: ['./user-modal.component.scss']
})
export class UserModalComponent implements OnInit {
  UserInputModel: UserInputModel = new UserInputModel();
  // 声明订阅对象
  subscript: Subscription;

  @Output()
  initFun = new EventEmitter<any>();

  constructor(
    private commonService: CommonService,
    private baseDataService: BaseDataService,
  ) { }

  ngOnInit() {
  }

  // 将初始化周期从父页面加载(主子页面一致加载)转移到子页面弹出框出现
  childInit(id: any) {
    // 获取用户
    if (id) {
      this.getUserByID(id);
    }
  }

  // 保存
  onSaveUser() {
    this.baseDataService.ModifyUser(this.UserInputModel)
      .subscribe((params) => {
        this.commonService.showSuccess('保存成功');
        this.UserInputModel = new UserInputModel();
        this.initFun.emit();
        $('#user').modal('hide');
      });
  }

  // 获取详情
  private getUserByID(Id: any) {
    this.baseDataService.GetUser(Id)
      .subscribe((params) => {
        this.UserInputModel = params;
      });
  }
}

import { Component, OnInit, ViewChild } from '@angular/core';
import { PageModel } from 'src/app/shared/model/page.model';
import { BaseDataService } from 'src/app/shared/services/base-data.service';
import { UserInputModel, UserOutputModel } from 'src/app/shared/model/User.model';
import { UserModalComponent } from './modals/user-modal.component';
import { Subscription } from 'rxjs';
import { CommonService } from './../../shared/services/common.service';
import { EventManager } from '@angular/platform-browser';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  UserInputModel: UserOutputModel = new UserOutputModel();
  UserOutputModel: PageModel<UserOutputModel> = new PageModel<UserOutputModel>();
  @ViewChild(UserModalComponent, { static: false })
  UserModalComponent: UserModalComponent;


  // 声明订阅对象
  subscript: Subscription;

  constructor(
    private baseDataService: BaseDataService,
    private commonService: CommonService,
    private eventManager: EventManager,
  ) { }

  ngOnInit() {
    this.getListPageUser();

    this.eventManager.addGlobalEventListener('window', 'keyup.esc', () => {
      this.onClearUser();
    });
    this.eventManager.addGlobalEventListener('window', 'keyup.enter', () => {
      this.onQueryUser();
    });
  }

  // 查询用户
  onQueryUser() {
    this.getListPageUser();
  }

  // 清空查询用户条件
  onClearUser() {
    this.UserInputModel = new UserOutputModel();
  }

  // 用户新建
  onNewUser() {
    $('#user').modal('show');
    this.UserModalComponent.childInit(null);
  }

  // 用户修改
  goDetailUser(item: any) {
    $('#user').modal('show');
    this.UserModalComponent.childInit(item.userID);
  }

  // 用户全选框控制数据列表checkbox
  selectAllUser(ev: any) {
    if (ev.target.checked) {
      this.UserOutputModel.listData.forEach(item => item.isCheck = true);
    } else {
      this.UserOutputModel.listData.forEach(item => item.isCheck = false);
    }
  }

  // 用户数据checkbox选择控制是否全选
  checkEventUser() {
    if (this.UserOutputModel.listData && this.UserOutputModel.listData.length !== 0) {
      return this.UserOutputModel.listData.findIndex(x => !x.isCheck) === -1;
    }
  }

  // 用户删除弹框确认事件
  onConfirmUser() {
    const retList = this.UserOutputModel.listData.filter(x => x.isCheck).map(x => x.userID);
    this.delUserFun(retList);
    $('#delete').modal('hide');
  }

  // 用户列表分页
  onPageChangeUser(pageInfo: any) {
    this.UserInputModel.pageNO = pageInfo.pageNO;
    this.UserInputModel.pageSize = pageInfo.pageSize;
    setTimeout(() => {
      this.getListPageUser();
    });
  }

  // 获取用户
  getListPageUser() {
    this.baseDataService.ListPageUser(this.UserInputModel)
      .subscribe((params) => {
        this.UserOutputModel = params;
      });
  }

  // 根据List<Guid>批量删除用户
  private delUserFun(selectTableList: any) {
    this.baseDataService.DeleteUser(selectTableList).subscribe((params) => {
      this.commonService.showSuccess('删除成功');
      this.getListPageUser();
    });
  }
}

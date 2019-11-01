export class BasePageModel {
  constructor() {
    this.pageNO = 1;
    this.pageSize = 10;
  }

  public pageSize: number;
  public pageNO: number;
  public isCheck: boolean;
}

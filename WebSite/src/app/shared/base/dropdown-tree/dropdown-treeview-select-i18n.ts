/**
 * Created by Zoe on 2018/12/29.
 */
import { TreeviewItem, TreeviewSelection, TreeviewI18nDefault } from 'ngx-treeview';
import { Injectable } from '@angular/core';

@Injectable()
export class DropdownTreeviewSelectI18n extends TreeviewI18nDefault {
  private internalSelectedItem: TreeviewItem;

  set selectedItem(value: TreeviewItem) {
    if (value) {
      this.internalSelectedItem = value;
    }
  }

  get selectedItem(): TreeviewItem {
    return this.internalSelectedItem;
  }

  getText(selection: TreeviewSelection): string {
    return this.internalSelectedItem ? this.internalSelectedItem.text : 'All';
  }

  getAllCheckboxText(): string {
    return '';
  }

  getFilterPlaceholder(): string {
    return '';
  }
  getFilterNoItemsFoundText(): string {
    return '';
  }
  getTooltipCollapseExpandText(isCollapse: boolean): string {
    return '';
  }
}

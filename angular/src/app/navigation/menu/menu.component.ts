import { Component } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: []
})
export class MenuComponent {
  public isCollapsed: boolean;

  constructor() {
    this.isCollapsed = true;
  }

  get totalItensInCart(): number{
    return 0;
  }
}

import { Component, OnInit } from '@angular/core';

declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: '/Collections', title: 'Collection',  icon: 'list', class: '' },
    { path: '/MyCollection', title: 'My Collection',  icon: 'list', class: '' },
    { path: '/Movies', title: 'Movies',  icon:'movie', class: '' },
    { path: '/Movie', title: 'Movie',  icon:'movie_filter', class: '' },
    { path: '/Contact', title: 'Contact',  icon:'contact_page', class: '' },
    { path: '/Users', title: 'Users',  icon:'person', class: '' },
    //{ path: '/Role', title: 'Roles',  icon:'enhanced_encryption', class: '' },
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor() { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };
}

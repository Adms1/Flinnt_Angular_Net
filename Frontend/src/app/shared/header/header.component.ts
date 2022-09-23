import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Input() roleId:number = 0;
  @Input() userObj:any = {};
  menuClicked = false;
  constructor(private route: Router) { }

  ngOnInit(): void {
  }

  onLogout(){
    localStorage.clear();
    this.route.navigate(['']);
  }

  onAncherClick(event? : Event){
    const menu = event.currentTarget["parentElement"].querySelector(".user_menu");
    if(!this.menuClicked){
      this.menuClicked = true;
      menu.classList.add("display-menu");
    }
    else{
      this.menuClicked = false;
      menu.classList.remove("display-menu");
    }
  }
}

import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private jwtHelper: JwtHelperService) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | Promise<boolean> {
    const token = localStorage.getItem("access_token");
    if (!!token) {

      if (!this.jwtHelper.isTokenExpired(token)) {
        // token valid
        return true;
      }
    }
    // token expired 
    this.router.navigate(['home']);
    return false;
  }
}
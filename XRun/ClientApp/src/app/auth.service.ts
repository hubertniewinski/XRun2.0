import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  userData: LoginResponse | undefined;

  constructor(
    private readonly http: HttpClient,
    @Inject('BASE_URL') private readonly baseUrl: string
  ) {}

  isLoggedIn() {
    return this.userData !== undefined;
  }

  isAdmin() {
    return this.userData?.isAdmin === true;
  }

  getName() {
    return this.userData?.name;
  }

  getToken() {
    return this.userData?.token;
  }

  getId() {
    return this.userData?.id;
  }

  setUserData(data: LoginResponse) {
    this.userData = data;
  }

  login(login: string, password: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http
      .post<any>(
        this.baseUrl + 'api/auth',
        {
          Login: login,
          Password: password,
        },
        { headers }
      );
  }
}

interface LoginResponse {
  token: string;
  isAdmin: boolean;
  name: string;
  id: string;
}

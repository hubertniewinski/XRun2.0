import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(private router: Router) { }

  getContainerClass() {
    const path = this.getPath();

    if(path == 'login'){
      return 'login-content-container';
    }
    else if(path == '') {
      return 'home-content-container';
    }

    return 'content-container';
  }

  private getPath() {
    return this.router.url.split('/').pop();
  }
}

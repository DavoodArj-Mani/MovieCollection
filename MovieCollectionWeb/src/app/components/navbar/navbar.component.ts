import { Component, OnInit, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ROUTES } from '../sidebar/sidebar.component';
import {Location, LocationStrategy, PathLocationStrategy} from '@angular/common';
import { Router } from '@angular/router';
import { RestApiService } from '../../../app/shared/rest-api.service';
import { LoginResp } from 'app/shared/view-entity/login-resp';
import { User } from 'app/shared/view-entity/user';
import { SharedService } from 'app/shared/shared-service';
declare var $: any;
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
    showLoginModal: boolean;
    registerForm: FormGroup;
    submitted = false;
    @Input() loginDetails = {UserName: '', Password: ''}
    loginResp: LoginResp;
    logedin = false;
    sharedService: SharedService;


    private listTitles: any[];
    location: Location;
    mobile_menu_visible: any = 0;
    private toggleButton: any;
    private sidebarVisible: boolean;

    

    constructor(location: Location,  private element: ElementRef, 
        private router: Router,private formBuilder: FormBuilder,
        public restApi: RestApiService,sharedService: SharedService) {
        this.location = location;
        this.sidebarVisible = false;
        this.sharedService = sharedService;
    }
    showLogin()
    {
      this.showLoginModal = true; 
    }
    hideLogin()
    {
      this.showLoginModal = false;
    }


    ngOnInit(){
        if(localStorage.getItem('Token') === null)
        {
            this.showLoginModal = true; 
        }
        else{
            this.sharedService.setUserName(localStorage.getItem('UserName'));
            this.sharedService.setLoginFlag(true);
        }

        this.registerForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(6)]]
        });

      this.listTitles = ROUTES.filter(listTitle => listTitle);
      const navbar: HTMLElement = this.element.nativeElement;
      this.toggleButton = navbar.getElementsByClassName('navbar-toggler')[0];
      this.router.events.subscribe((event) => {
        this.sidebarClose();
         var $layer: any = document.getElementsByClassName('close-layer')[0];
         if ($layer) {
           $layer.remove();
           this.mobile_menu_visible = 0;
         }
     });
    }

    sidebarOpen() {
        const toggleButton = this.toggleButton;
        const body = document.getElementsByTagName('body')[0];
        setTimeout(function(){
            toggleButton.classList.add('toggled');
        }, 500);

        body.classList.add('nav-open');

        this.sidebarVisible = true;
    };
    sidebarClose() {
        const body = document.getElementsByTagName('body')[0];
        this.toggleButton.classList.remove('toggled');
        this.sidebarVisible = false;
        body.classList.remove('nav-open');
    };
    sidebarToggle() {
        // const toggleButton = this.toggleButton;
        // const body = document.getElementsByTagName('body')[0];
        var $toggle = document.getElementsByClassName('navbar-toggler')[0];

        if (this.sidebarVisible === false) {
            this.sidebarOpen();
        } else {
            this.sidebarClose();
        }
        const body = document.getElementsByTagName('body')[0];

        if (this.mobile_menu_visible == 1) {
            // $('html').removeClass('nav-open');
            body.classList.remove('nav-open');
            if ($layer) {
                $layer.remove();
            }
            setTimeout(function() {
                $toggle.classList.remove('toggled');
            }, 400);

            this.mobile_menu_visible = 0;
        } else {
            setTimeout(function() {
                $toggle.classList.add('toggled');
            }, 430);

            var $layer = document.createElement('div');
            $layer.setAttribute('class', 'close-layer');


            if (body.querySelectorAll('.main-panel')) {
                document.getElementsByClassName('main-panel')[0].appendChild($layer);
            }else if (body.classList.contains('off-canvas-sidebar')) {
                document.getElementsByClassName('wrapper-full-page')[0].appendChild($layer);
            }

            setTimeout(function() {
                $layer.classList.add('visible');
            }, 100);

            $layer.onclick = function() { //asign a function
              body.classList.remove('nav-open');
              this.mobile_menu_visible = 0;
              $layer.classList.remove('visible');
              setTimeout(function() {
                  $layer.remove();
                  $toggle.classList.remove('toggled');
              }, 400);
            }.bind(this);

            body.classList.add('nav-open');
            this.mobile_menu_visible = 1;

        }
    };

    getTitle(){
      var titlee = this.location.prepareExternalUrl(this.location.path());
      if(titlee.charAt(0) === '#'){
          titlee = titlee.slice( 1 );
      }

      for(var item = 0; item < this.listTitles.length; item++){
          if(this.listTitles[item].path === titlee){
              return this.listTitles[item].title;
          }
      }
      return 'Dashboard';
    }
    get f() { return this.registerForm.controls; }

    getUserName(){
        console.log ("userName ", this.sharedService.getUserName()); 
        return this.sharedService.getUserName();
    }
    getLoginFlag()
    {
        return this.sharedService.getLoginFlag();
    }
    onSubmit(submitterId:string) {
        this.submitted = true;
        if (this.registerForm.invalid) {
            return;
        }
        else
        {
            if(submitterId == "LogIn")
            {
                this.loginDetails.UserName = this.registerForm.get('email').value;
                this.loginDetails.Password = this.registerForm.get('password').value;
                this.restApi.Login(this.loginDetails).subscribe((data: LoginResp) =>{
                    this.showLoginModal = false;
                    
                    this.loginResp = data;
                    localStorage.setItem('Token', data.token);
                    localStorage.setItem('UserName', data.user_name);
                    this.sharedService.setUserName(data.user_name);
                    this.sharedService.setLoginFlag(true);

                    this.showNotification('bottom','right','Login Success')
                });
                
            }
            else{
                this.loginDetails.UserName = this.registerForm.get('email').value;
                this.loginDetails.Password = this.registerForm.get('password').value;
                this.restApi.SignUp(this.loginDetails).subscribe((data: User) =>{
                    this.showNotification('bottom','right','Registration Success')
                    this.restApi.Login(this.loginDetails).subscribe((data: LoginResp) =>{
                        this.showLoginModal = false;
                        this.loginResp = data;
                        localStorage.setItem('Token', data.token);
                        localStorage.setItem('UserName', data.user_name);

                        this.sharedService.setUserName(data.user_name);
                        this.sharedService.setLoginFlag(true);

                        this.showNotification('bottom','right','Login Success')
                    });
                });
            }
            this.submitted = false;
            
        }
    }

    showNotification(from, align,message){
        //const type = ['','info','success','warning','danger'];
        $.notify({
            icon: "notifications",
            message: message
    
        },{
            type: 'success',
            timer: 4000,
            placement: {
                from: from,
                align: align
            },
            template: '<div data-notify="container" class="col-xl-4 col-lg-4 col-11 col-sm-4 col-md-4 alert alert-{0} alert-with-icon" role="alert">' +
              '<button mat-button  type="button" aria-hidden="true" class="close mat-button" data-notify="dismiss">  <i class="material-icons">close</i></button>' +
              '<i class="material-icons" data-notify="icon">notifications</i> ' +
              '<span data-notify="title">{1}</span> ' +
              '<span data-notify="message">{2}</span>' +
              '<div class="progress" data-notify="progressbar">' +
                '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
              '</div>' +
              '<a href="{3}" target="{4}" data-notify="url"></a>' +
            '</div>'
        });
    }
}

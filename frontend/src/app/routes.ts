import { Routes } from "@angular/router";
import { HomeComponent } from "./components/home/home.component";
import { LoggedPageComponent } from "./components/logged-page/logged-page.component";
import { UserProfileComponent } from "./components/user-profile/user-profile.component";
import { HomeLogPageComponent } from "./components/home-log-page/home-log-page.component";
import { PostAdFormComponent } from "./components/post-ad-form/post-ad-form.component";
import { LoginComponent } from "./components/login/login.component";
const routeConfig: Routes = [
    {
        path: '',
        component: HomeComponent,
        title: 'Home'
    },
    {
        path: 'logged-page',
        component: LoggedPageComponent,
        title: 'Logged Page'
    },
    {
        path: 'user-profile',
        component: UserProfileComponent,
        title: 'User Profile'
    },
    {
        path: 'home-log-page',
        component: HomeLogPageComponent,
        title: 'Home Log Page'
    },
   
    {
        path: 'post-ad-form',
        component: PostAdFormComponent,
        title: 'Post Advertisement Form'
    }
    ,
    {
        path: 'login',
        component: LoginComponent,
        title: 'login'
    }
   


];

export default routeConfig;
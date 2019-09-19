import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './_shared/home/home.component';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';
import { TestapiComponent } from './testapi/testapi.component';
import { WordsComponent } from './words/words.component';
import { NotfoundComponent } from './_shared/notfound/notfound.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'notfound', component: NotfoundComponent },
    { path: 'login', component: LoginComponent },
    { path: 'testapi', component: TestapiComponent },
    { path: 'words', component: WordsComponent, canActivate: [AuthGuard] },
    

    // otherwise redirect to home
    { path: '**', redirectTo: 'notfound' }
];

export const appRoutingModule = RouterModule.forRoot(routes);
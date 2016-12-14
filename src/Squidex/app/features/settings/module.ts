/*
 * Squidex Headless CMS
 * 
 * @license
 * Copyright (c) Sebastian Stehle. All rights reserved
 */

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SqxFrameworkModule, SqxSharedModule } from 'shared';

import {
    ClientComponent,
    ClientsPageComponent,
    ContributorsPageComponent,
    LanguagesPageComponent,
    SettingsAreaComponent
} from './declarations';

const routes: Routes = [
    {
        path: '',
        component: SettingsAreaComponent,
        children: [
            {
                path: ''
            },
            {
                path: 'clients',
                component: ClientsPageComponent
            }, {
                path: 'contributors',
                component: ContributorsPageComponent
            }, {
                path: 'languages',
                component: LanguagesPageComponent
            }
        ]
    }
];

@NgModule({
    imports: [
        SqxFrameworkModule,
        SqxSharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        ClientComponent,
        ClientsPageComponent,
        ContributorsPageComponent,
        LanguagesPageComponent,
        SettingsAreaComponent
    ]
})
export class SqxFeatureSettingsModule { }
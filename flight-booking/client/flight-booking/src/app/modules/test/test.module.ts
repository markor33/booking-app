import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { TestComponent } from './test/test.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'test', component: TestComponent}
];

@NgModule({
  declarations: [
    TestComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
  ]
})
export class TestModule { }

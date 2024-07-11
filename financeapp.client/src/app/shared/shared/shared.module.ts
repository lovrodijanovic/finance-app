import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LazyLoadDirective } from '../directives/lazy-load.directive';



@NgModule({
  declarations: [
    LazyLoadDirective],
  imports: [
    CommonModule,
  ],
  exports: [CommonModule, LazyLoadDirective]
})
export class SharedModule { }

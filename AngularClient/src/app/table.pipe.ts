import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ 
    name: 'filterAll'
})
export class FilterPipe implements PipeTransform {
  transform(value: any, searchText: any): any {
    if(!searchText) {
      return value;
    }
    return value.filter((data: any) => this.matchValue(data,searchText)); 
  }

  matchValue(data: any, value: any) {
    return Object.keys(data).map((key) => {
       return new RegExp(value, 'gi').test(data);
    }).some(result => result);
  }
 }
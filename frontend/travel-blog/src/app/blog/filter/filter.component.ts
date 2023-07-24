import { Component, OnInit, ViewChildren, ElementRef, QueryList } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { HttpService } from 'src/app/services/http.service';
import { Users } from '../model/user';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent  {
  @ViewChildren('checkboxInput') checkboxInputs!: QueryList<ElementRef<HTMLInputElement>>;
    form: FormGroup;
    authors?:Users
    comboboxIds = ['datePosted', 'expirationDate', 'category', 'city', 'store']; 
    selectedValues: { [key: string]: any } = {}; 

  constructor(fb: FormBuilder,private readonly httpService:HttpService) {
    this.form = fb.group({
     selectedAuthors:  new FormArray([])
    });
    this.comboboxIds.forEach((id) => {
      this.selectedValues[id] = 'All';
    })
  }
    ngOnInit() {
       this.httpService.getUsers().subscribe(data=>
       this.authors = data.filter((user) => user.isCreator === true)); 
    }
    onCheckboxChange(event: any) {
    
    const selectedAuthors = (this.form.controls['selectedAuthors'] as FormArray);
    if (event.target.checked) {
      selectedAuthors.push(new FormControl(event.target.value));
    } else {
      const index = selectedAuthors.controls
      .findIndex(x => x.value === event.target.value);
      selectedAuthors.removeAt(index);
    }
  }

  selectChangeHandler (event: any) {
    this.selectedValues[event.target.id] = event.target.value;
  }

  resetComboboxes() {
   this.comboboxIds.forEach((id) => {
    const element = document.getElementById(id) as HTMLSelectElement | null;
    if (element) {
      element.value = "all";
    }
    
  });
    this.checkboxInputs.forEach(input => {
      input.nativeElement.checked = false;
    });
     const selectedAuthors = this.form.get('selectedAuthors') as FormArray;
     selectedAuthors.clear();
   
  }

  submit() {
    console.log("Authors:\n");
    console.log(this.form.value);
    console.log("Optiuni:\n");
    console.log(this.selectedValues);
    
  }
}

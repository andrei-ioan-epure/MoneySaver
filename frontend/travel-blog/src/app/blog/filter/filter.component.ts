import {
  Component,
  OnInit,
  ViewChildren,
  ElementRef,
  QueryList,
  Input,
  Output,
  EventEmitter,
} from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { HttpService } from 'src/app/services/http.service';
import { Users } from '../model/user';
import { Articles } from '../model/article';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent {
  @Input() filteredArticles?: Articles;

  @Output() dataEmitter: EventEmitter<Articles> = new EventEmitter<Articles>();

  @ViewChildren('checkboxInput') checkboxInputs!: QueryList<
    ElementRef<HTMLInputElement>
  >;

  form: FormGroup;
  authors?: Users;
  comboboxIds = ['datePosted', 'expirationDate', 'category', 'city', 'store'];
  selectedValues: { [key: string]: any } = {};

  constructor(fb: FormBuilder, private readonly httpService: HttpService) {
    this.form = fb.group({
      selectedAuthors: new FormArray([]),
    });
    this.comboboxIds.forEach((id) => {
      this.selectedValues[id] = 'All';
    });
  }

  ngOnInit() {
    this.httpService
      .getUsers()
      .subscribe(
        (data) =>
          (this.authors = data.filter((user) => user.isCreator === true))
      );
  }

  onCheckboxChange(event: any) {
    const selectedAuthors = this.form.controls['selectedAuthors'] as FormArray;
    if (event.target.checked) {
      selectedAuthors.push(new FormControl(event.target.value));
    } else {
      const index = selectedAuthors.controls.findIndex(
        (x) => x.value === event.target.value
      );
      selectedAuthors.removeAt(index);
    }
  }

  selectChangeHandler(event: any) {
    this.selectedValues[event.target.id] = event.target.value;
  }

  resetFields() {
    this.comboboxIds.forEach((id) => {
      const element = document.getElementById(id) as HTMLSelectElement | null;
      if (element) {
        element.value = 'all';
      }
    });
    this.checkboxInputs.forEach((input) => {
      input.nativeElement.checked = false;
    });
    const selectedAuthors = this.form.get('selectedAuthors') as FormArray;
    selectedAuthors.clear();

    this.httpService.getFilteredArticles().subscribe((data) => {
      console.log(data);
      this.dataEmitter.emit(data);
    });
  }

  submit() {
    this.selectedValues['authors'] = this.form.get('selectedAuthors')?.value;
    // console.log("Info:");
    // console.log(this.selectedValues);

    this.httpService
      .getFilteredArticles(
        this.selectedValues['authors'].join(','),
        this.selectedValues['category'],
        this.selectedValues['city'],
        this.selectedValues['store'],
        this.selectedValues['datePosted'],
        this.selectedValues['expirationDate']
      )
      .subscribe((data) => {
        //console.log(data)
        this.dataEmitter.emit(data);
      });
  }
}

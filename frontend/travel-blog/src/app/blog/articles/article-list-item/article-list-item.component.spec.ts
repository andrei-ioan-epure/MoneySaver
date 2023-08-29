import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleListItemComponent } from './article-list-item.component';

describe('ArticleListItemComponent', () => {
  let component: ArticleListItemComponent;
  let fixture: ComponentFixture<ArticleListItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ArticleListItemComponent]
    });
    fixture = TestBed.createComponent(ArticleListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

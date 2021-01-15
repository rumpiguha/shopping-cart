import { TestBed, async, inject } from '@angular/core/testing';
import { Router } from '@angular/router';
import { AppComponent } from './app.component';
import { SharedService } from './services/shared-service';
import { RouterTestingModule } from '@angular/router/testing';

describe('AppComponent', () => {
    beforeEach(async(() => {
      TestBed.configureTestingModule({
        imports: [RouterTestingModule],
        declarations: [
          AppComponent
        ],
        providers: [SharedService]
      }).compileComponents();
    }));
  it('should create the app', async(inject([SharedService, Router], (sharedService: SharedService, router: Router) => {
      const fixture = TestBed.createComponent(AppComponent);
      const app = fixture.debugElement.componentInstance;
      expect(app).toBeTruthy();
    })));
  });

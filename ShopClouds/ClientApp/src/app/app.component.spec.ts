import { TestBed, async, inject } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { SharedService } from './services/shared-service';

describe('AppComponent', () => {
    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [
          AppComponent
        ],
        providers: [SharedService]
      }).compileComponents();
    }));
    it('should create the app', async(inject([SharedService],(sharedService: SharedService) => {
      const fixture = TestBed.createComponent(AppComponent);
      const app = fixture.debugElement.componentInstance;
      expect(app).toBeTruthy();
    })));
  });
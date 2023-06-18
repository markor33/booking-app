import { Component } from '@angular/core';
import { ApiKeyService } from '../services/api-key.service';
import { ApiKey } from '../models/api-key.model';

@Component({
  selector: 'app-api-key',
  templateUrl: './api-key.component.html',
  styleUrls: ['./api-key.component.css']
})
export class ApiKeyComponent {

  apiKey: ApiKey | null = null;
  isPermanent: boolean = false;

  constructor(private apiKeyService: ApiKeyService) { }

  ngOnInit() {
    this.apiKeyService.get().subscribe({
      next: (apiKey) => {
        this.apiKey = apiKey;
        if (apiKey.expireDate !== null)
          this.isPermanent = false;
        else
          this.isPermanent = true;
      },
      error: (err) => this.apiKey = null      
    });
  }

  create() {
    this.apiKeyService.create(this.isPermanent).subscribe((apiKey) => this.apiKey = apiKey);
  }

  update() {
    console.log('asdasd');
    this.apiKeyService.update(this.apiKey as ApiKey, this.isPermanent).subscribe((apiKey) => this.apiKey = apiKey)
  }

  refresh() {
    this.apiKeyService.refresh(this.apiKey as ApiKey).subscribe((apiKey) => this.apiKey = apiKey);
  }

  delete() {
    this.apiKeyService.delete(this.apiKey as ApiKey).subscribe((res) => this.apiKey = null);
  }

}

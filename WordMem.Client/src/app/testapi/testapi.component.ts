import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-testapi',
  templateUrl: './testapi.component.html',
  styleUrls: ['./testapi.component.less']
})
export class TestapiComponent implements OnInit {

  values:any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
  
    this.http.get(`${environment.apiUrl}/api/values`).subscribe(response => {
      this.values = response;
    }, err => {
      console.log(err)
    });
  }

}

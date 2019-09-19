import { Component, OnInit } from '@angular/core';
import { environment } from '@environments/environment';
import { Word } from '@app/_models/word';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-words',
  templateUrl: './words.component.html',
  styleUrls: ['./words.component.less']
})
export class WordsComponent implements OnInit {

words:any

  constructor(private http: HttpClient) { }

  ngOnInit() {
    let currentUser = JSON.parse(localStorage.getItem("currentUser")) ;
    console.log(currentUser);
    let token = currentUser.token;
    console.log(token);
    this.http.get(`${environment.apiUrl}/api/words`).subscribe(response => {
      this.words = response;
      console.log(response);
    }, err => {
      console.log(err+"hata var")
    });
  }

}

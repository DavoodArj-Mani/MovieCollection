import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Notify } from 'app/shared/notification';
import { RestApiService } from 'app/shared/rest-api.service';
import { SharedService } from 'app/shared/shared-service';
import { Contact } from 'app/shared/view-entity/contact';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  contactForm: FormGroup;
  contact: Contact;
  contactId;


  constructor(public sharedService: SharedService, public restApi: RestApiService, private notification: Notify,
    private formBuilder: FormBuilder, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.contactForm = this.formBuilder.group({
      userId: [{ value: '', disabled: true }],
      userName: [{ value: '', disabled: true }],
      firstName: [''],
      lastName: [''],
      birthDate: [''],
      gender: ['']
    })
    this.QueryContact();
  }
  UpdateForm() {
    this.contactForm = this.formBuilder.group({
      userId: [{ value: this.contact.userId, disabled: true }],
      userName: [{ value: this.contact.userName, disabled: true }],
      firstName: this.contact.firstName,
      lastName: this.contact.lastName,
      birthDate: this.contact.birthDate,
      gender: this.contact.gender.toString()
    })
  }
  QueryContact() {

    this.restApi.QueryContactByUserName(this.sharedService.getUserName()).subscribe((data) => {
      this.contact = data;
      this.contactId = this.contact.contactId;
      this.UpdateForm();
    });
  }
  get f() { return this.contactForm.controls; }
  submitForm() {
    this.contact = new Contact();
    if (this.contactForm.get('userId').value)
      this.contact.userId = this.contactForm.get('userId').value;
    if (this.contactForm.get('userName').value)
      this.contact.userName = this.contactForm.get('userName').value;
    if (this.contactForm.get('firstName').value)
      this.contact.firstName = this.contactForm.get('firstName').value
    if (this.contactForm.get('lastName').value)
      this.contact.lastName = this.contactForm.get('lastName').value
    if (this.contactForm.get('birthDate').value)
      this.contact.birthDate = new Date(this.contactForm.get('birthDate').value);
    if (this.contactForm.get('gender').value)
      this.contact.gender = Number(this.contactForm.get('gender').value )


    if (this.contactId != null) // Update
    {
      this.contact.contactId = this.contactId;
      console.log ("Update", this.contact); 
      this.restApi.UpdateContact(this.contact).subscribe((data) => {
        this.contact = data;
        this.UpdateForm();
        this.notification.showNotification('bottom', 'right', 'Contact successfully Updated.', 'success');
      });

    }
    else // Create
    {
      console.log ("Create", this.contact); 
      this.restApi.CreateContact(this.contact).subscribe((data) => {
        this.contact = data;
        this.UpdateForm();
        this.notification.showNotification('bottom', 'right', 'Contact successfully Created.', 'success');
      });

    }
  }

}

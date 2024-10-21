import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {
  userForm!: FormGroup;
  user!: User;
  currentFirstName?: string = "";
  currentLastName?: string = "";

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,

  ) { }

  ngOnInit(): void {
    this.initializeForm();
    this.loadUserData();
  }

  // Inizializza il FormGroup
  initializeForm(): void {
    console.log('initialize form');
    this.userForm = this.fb.group({
      id: [{ value: '', disabled: true }],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      country: ['', Validators.required],
      city: ['', Validators.required],
      isEnabled: [true],
      requestToBeForgotten: [false],
      consentForDataProcessing: [false],
      consentForReceivingPromotionalMessages: [false]
    });
    console.log('end initialization');

    // Listner for checkbox requestToBeForgotten changes
    this.userForm.get('requestToBeForgotten')?.valueChanges.subscribe((checked: boolean) => {
      if (checked) {
        this.currentFirstName = this.userForm.get('firstName')?.value;
        this.currentLastName = this.userForm.get('lastName')?.value;

        this.userForm.get('firstName')?.setValue('');
        this.userForm.get('lastName')?.setValue('');

        // Disabled fields FirstName and LastName
        this.userForm.get('firstName')?.disable();
        this.userForm.get('lastName')?.disable();
      } else {
        // Enabled fields FirstName and LastName
        this.userForm.get('firstName')?.enable();
        this.userForm.get('lastName')?.enable();

        console.log('currentLastName: ' + this.currentLastName);
        if (this.currentLastName != null && this.currentLastName.length > 0) {
          console.log('currentLastName: ' + this.currentLastName);
          this.userForm.get('firstName')?.setValue(this.currentFirstName);
          this.userForm.get('lastName')?.setValue(this.currentLastName);
        }

      }
    });


  }

  // Load user data from API
  loadUserData(): void {
    console.log('load usera data');
    const userId = this.route.snapshot.params['id']; // UserId from the route
    console.log('user id: ' + userId);

    if (userId != null)
    {
      this.userService.getUserById(userId).subscribe((user: User) => {
        this.user = user;
        this.userForm.patchValue(this.user);
        this.currentFirstName = user.firstName;
        this.currentLastName = user.lastName;
      });
    }
    else {
      const newUserCreate: User = {
        id: undefined,
        firstName: '',
        lastName: '',
        dateOfBirth: undefined,
        country: '',
        city: '',
        isEnabled: true,
        requestToBeForgotten: false,
        consentForDataProcessing: false,
        consentForReceivingPromotionalMessages: false,
      };
      this.user = newUserCreate;

      this.userForm.patchValue(this.user);
    }

    console.log('end loading user data');
  }

  onSave(): void {
    if (this.userForm.valid) {
      const savedUser: User = {
        ...this.userForm.getRawValue(), //Read all the parameters
        id: this.user.id //And add is of the user
      };

      console.log("new userId: " + savedUser.id);
      if (savedUser.id != null) {
        this.userService.updateUser(savedUser).subscribe(() => {
          this.router.navigate(['/users']);
        });
      }
      else {
        this.userService.createUser(savedUser).subscribe(() => {
          this.router.navigate(['/users']);
        });
      }
    }
  }

  onClose(): void {
    this.router.navigate(['/users']);
  }
}

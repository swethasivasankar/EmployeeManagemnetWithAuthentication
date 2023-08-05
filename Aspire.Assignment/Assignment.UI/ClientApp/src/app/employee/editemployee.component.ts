import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first, pipe } from 'rxjs';
import { Employee } from '../_models/employee';
import { EmployeeService } from '../_services/employee.service';
import { AddEmployee } from '../_models/addEmployee';
import { ReferenceTbl } from '../_models/referenceTbl';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from '../_services/alert.service';
import { User } from '../_models/user';


@Component({ templateUrl: 'editemployee.component.html',
styleUrls: ['editemployee.component.css']
 })

export class EditemployeeComponent  implements OnInit {
  
  EmployeeForm!: FormGroup;
  data: any;

  id: any ;
  title!: string;
  loading = false;
  submitting = false;
  submitted = false;
  employee: any ;
  employees: Employee[] = [];
  references:ReferenceTbl[]= [];
  addemployeeobj: AddEmployee = new AddEmployee();
  genderdata!: any;
  practicedata!: any;
  usernamedata!:any;
  empDesignationData!: any;
  empTypeData!: any;
  users:User[]=[];
  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private employeeService: EmployeeService,
      private alertService: AlertService,
      private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() : void {
    
      this.EmployeeForm = this.formBuilder.group({
        employeeDetailId:[''],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        dob: ['', Validators.required],
        genderRefId: ['', Validators.required],
        contactNumber: ['', Validators.required],
        alternateNumber: [''],
        email:['', Validators.required],
        address:['', Validators.required],
        city:['', Validators.required],
        state:['', Validators.required],
        country:['', Validators.required],
        zip:[],
        employeeNumber:['', Validators.required],
        empDesignationId:['', Validators.required],
        practiceRefId:['', Validators.required],
        doj:['', Validators.required],
        empTypeId:['', Validators.required],
        userId:['', ]

      });

      this.title = 'Edit Employee';
      this.activatedRoute.params.subscribe(data => {
        this.id = data.id
      })
     // to fetch practice, gender, employee designation, emptype value.
      this.employeeService.getAllReference()
      .subscribe(references => {this.references = references; 
      console.log(references)});

      // to fetch user detail 
        this.employeeService.getAllUser()
        .subscribe(users =>{this.users= users})

      //to fetch employee record by Id,
      this.employeeService.getById(this.id).subscribe(data => {
        this.employee= data;  
        console.log(data);
        
      // to get fetch all the detail of employee along with gender and practice value
      this.employeeService.getAll()
        .subscribe(employees => {this.employees = employees
          // for each to iterate geneder and practice value
        this.employees.forEach((empl)=>
        {
          // console.log(empl)
          this.references.forEach((ref)=>{  
            //console.log(ref)
         this.users.forEach((user)=>
         {
          // console.log(user)

        //  })
           if(empl.empDetailsID==this.employee.empDetailsID)
           {
            if(user.id == empl.userId)
            {
              this.employee.userName=user.username
              console.log(user.username)
              console.log( this.employee.userName)
            }



            if(ref.referenceId==empl.genderRefId)
              {
                this.employee.gender=ref.title;
                console.log(ref.title)
                console.log(this.employee.gender)               
              }

              else if(ref.referenceId==empl.practiceRefId)
              {
                this.employee.practice=ref.title
                // console.log(ref.title)
                // console.log(this.employee.practice)
              }
              else if(ref.referenceId==empl.empDesignationId)
              {
                empl.empDesignation=ref.title

              }
              else if(ref.referenceId==empl.empTypeId)
              {
                empl.empType=ref.title

              }
            }    
          });   
          })
        });
      });
    });
    // called getReference and get userdata method to values values in drop down
    this.getReference();
    this.getUserData();
  }
    // convenience getter for easy access to form fields
    get f() { return this.EmployeeForm.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.EmployeeForm.invalid) {
            return;
        }

        this.submitting = true;
        this.addemployeeobj.employeeDetailId = this.employee.empDetailsID;
       
        this.addemployeeobj.firstName = this.EmployeeForm.value.firstName;
        this.addemployeeobj.lastName = this.EmployeeForm.value.lastName;
        this.addemployeeobj.dob = this.EmployeeForm.value.dob;
        this.addemployeeobj.genderRefId = Number(this.EmployeeForm.value.genderRefId);
        this.addemployeeobj.contactNumber = this.EmployeeForm.value.contactNumber;
        this.addemployeeobj.alternateNumber = this.EmployeeForm.value.alternateNumber;
        this.addemployeeobj.email = this.EmployeeForm.value.email;
        this.addemployeeobj.address = this.EmployeeForm.value.address;
        this.addemployeeobj.city = this.EmployeeForm.value.city;
        this.addemployeeobj.state = this.EmployeeForm.value.state;
        this.addemployeeobj.country = this.EmployeeForm.value.country;
        this.addemployeeobj.zip = this.EmployeeForm.value.zip;
        this.addemployeeobj.employeeNumber = this.EmployeeForm.value.employeeNumber;
        this.addemployeeobj.empDesignationId = Number(this.EmployeeForm.value.empDesignationId);
        this.addemployeeobj.practiceRefId = Number(this.EmployeeForm.value.practiceRefId);
        this.addemployeeobj.doj = this.EmployeeForm.value.doj;
        this.addemployeeobj.empTypeId = Number(this.EmployeeForm.value.empTypeId);
        this.addemployeeobj.userId = Number(this.EmployeeForm.value.userId);

        console.log(this.addemployeeobj)
        this.employeeService.put(this.addemployeeobj)
            .subscribe({
                next: () => {
                    this.alertService.success('Employee detail updated', { keepAfterRouteChange: true });
                    this.router.navigateByUrl('/empl');
                    // console.log(this.EmployeeForm)

                },
                error: (error: any) => {
                    this.alertService.error(error);
                    this.submitting = false;
                }


            })
    }
getReference()
 {
  this.employeeService.getAllReference().subscribe((data: any[]) => {
    this.genderdata = data;
    this.practicedata= data;
    this.empDesignationData= data;
    this.empTypeData= data;

    //console.log(this.genderdata)
  });
 }

 getUserData()
    {
        this.employeeService.getAllUser().subscribe((data: any[])=>{
            this.usernamedata= data;
          //  console.log(this.usernamedata)

        });

    }

}

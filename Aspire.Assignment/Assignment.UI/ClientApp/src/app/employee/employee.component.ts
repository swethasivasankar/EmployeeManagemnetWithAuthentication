import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../_services/employee.service';
import { Employee } from '../_models/employee';
import { ReferenceTbl } from '../_models/referenceTbl';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AddEmployee } from '../_models/addEmployee';
import { AlertService } from '../_services/alert.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TokenResponse } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  templateUrl: './employee.component.html'
})
export class EmployeeComponent implements OnInit {
  employees: Employee[] = [];
  references:ReferenceTbl[]= [];
  data!: any;
  EmployeeForm!: FormGroup;
  empObj: Employee[]=[];
  addemployeeobj: AddEmployee = new AddEmployee();
  user: TokenResponse | null;

  

  constructor( 
    private formbuilder: FormBuilder,
    private employeeService: EmployeeService,
    private alertService: AlertService,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService
  ) {
    this.user = this.accountService.userValue;

  }
  ngOnInit() {

   //if it norml employee login should redirect to edit screen
   if(this.user?.roles == "Employee")
   {
     this.router.navigate(['../empl/edit/'+this.user.empDetailsID])
   } 

   this.getAllEmployee();


  }


  deleteEmployee(item: any) {
      //confirmation for product delete
      if (confirm('Are you sure to delete record?'))
        this.employeeService.delete(item.empDetailsID)
        .subscribe({
          next: () => {
              this.alertService.success('Employee detail deleted successfully', { keepAfterRouteChange: true });
              this.router.navigateByUrl('/empl');
              //for page refresh after delete
              window.location.reload();
          },
          error: (error: any) => {
              this.alertService.error(error);
          }

        });
      }

 getAllEmployee()
{
 // to fetch practice, gender, employee designation, emptype value.
 this.employeeService.getAllReference()
 .subscribe(references => {this.references = references; 
 console.log(references)});

 // to get fetch all the detail of employee along with gender and practice value
   this.employeeService.getAll()
 .subscribe(employees => {this.employees = employees
           this.employees.forEach((empl)=>{
             // console.log(empl)
             this.references.forEach((ref)=>{
               // console.log(ref)
             //  console.log(ref.referenceID)
             //  console.log(empl.genderRefId)
           if(ref.referenceId==empl.genderRefId )
             {
               empl.gender=ref.title;
               //console.log(empl.gender)
             }
             else if(ref.referenceId==empl.practiceRefId)
             {
               empl.practice=ref.title

             }
             else if(ref.referenceId==empl.empDesignationId)
             {
               empl.empDesignation=ref.title

             }
             else if(ref.referenceId==empl.empTypeId)
             {
               empl.empType=ref.title

             }
             
             
             })
             })
           });   


        
    }
   
}



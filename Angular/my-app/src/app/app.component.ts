import { Component, OnInit } from '@angular/core';
import { subscribeOn } from 'rxjs';
import { EmployeeService } from './service/employee.service';
import { Employee } from './models/employee';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  Employeeary : Employee[]=[];
  Employeeformgroup:FormGroup;

  constructor(private empservice:EmployeeService, private fb:FormBuilder){
    this.Employeeformgroup=this.fb.group({
      id:[""],
      name:[""],
      mobileNo:[""],
      emailID:[""],
    })
  }

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees(){
    this.empservice.GetEmployee().subscribe(response =>{
      console.log(response);
      this.Employeeary=response;
    })
  }

  OnSubmit(){
    console.log(this.Employeeformgroup.value); 
    if(this.Employeeformgroup.value.id != null && this.Employeeformgroup.value.id !=""){
      this.empservice.UpdateEmployee(this.Employeeformgroup.value).subscribe(Response=>{
        console.log(Response);
        this.getEmployees();
        this.Employeeformgroup.setValue({
          id:"",
          name:"",
          mobileNo:"",
          emailID:"",
        })
      })
    }
    else{
      this.empservice.AddEmployee(this.Employeeformgroup.value).subscribe(Response=>{
        console.log(Response);
        this.getEmployees();
        this.Employeeformgroup.setValue({
          id:"",
          name:"",
          mobileNo:"",
          emailID:"",
        })
      })    
    }


  }
   

  Fillform(emp:Employee){
    this.Employeeformgroup.setValue({
      id:emp.id,
      name:emp.name,
      mobileNo:emp.mobileNo,
      emailID:emp.emailID,
    })
  }

  DeleteEmp(id:string){
    this.empservice.DeleteEmployee(id).subscribe(res=>{
      console.log(res);
      this.getEmployees();
    })
  }

  title = 'my-app';
}

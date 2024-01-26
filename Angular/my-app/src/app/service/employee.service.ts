import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private Httpclient:HttpClient) { }

  baseurl="http://localhost:52701/api/Employee";

  GetEmployee(): Observable<Employee[]>{
    return this.Httpclient.get<Employee[]>(this.baseurl)
  }

  AddEmployee(emp:Employee):Observable<Employee>{
    emp.id="00000000-0000-0000-0000-000000000000";
    return this.Httpclient.post<Employee>(this.baseurl,emp)
  }

  UpdateEmployee(emp:Employee):Observable<Employee>{
    return this.Httpclient.put<Employee>(this.baseurl+'/'+emp.id,emp);
  }

  DeleteEmployee(id:string):Observable<Employee>{
    return this.Httpclient.delete<Employee>(this.baseurl+'/'+id);
  }

}

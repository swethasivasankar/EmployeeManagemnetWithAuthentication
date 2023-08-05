import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from '../_models/employee';
import { environment } from 'src/environments/environment';
import { Observable } from "rxjs";
import { ReferenceTbl } from '../_models/referenceTbl';
import { AddEmployee } from '../_models/addEmployee';
import { User } from '../_models/user';



@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

    public getAll() {
        return this.http.get<Employee[]>(`${environment.apiUrl}/api/Employee`);
    }

    public getById(id: number) {
        return this.http.get<Employee[]>("https://localhost:5001/api/Employee/"+id);
    }

    public post(employee: AddEmployee) : Observable<any> {
      console.log(employee);
      //return this.http.post<any>(`${environment.apiUrl}/api/Employee`, employee);
        
        return this.http.post('https://localhost:5001/api/Employee', employee);

    }

    public put(employee: AddEmployee) : Observable<any> {
      console.log(employee);

     // return this.http.put<any>('https://localhost:5001/api/Employee', employee);

      return this.http.put('https://localhost:5001/api/Employee', employee);

  }
  public delete(id: number) : Observable<any> {
    //return this.http.delete<any>(`${environment.apiUrl}/api/Employee/${id}`);
    return this.http.delete('https://localhost:5001/api/Employee/'+id);

}
// to fet values in the drop down
public getAllReference() {

  return this.http.get<ReferenceTbl[]>(`${environment.apiUrl}/api/Reference`);
}

public getAllUser() {

  return this.http.get<User[]>(`${environment.apiUrl}/api/Auth`);
}

} 

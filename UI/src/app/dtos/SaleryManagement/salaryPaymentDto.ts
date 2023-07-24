import { BaseEntityDto } from "../base/baseEntityDto";

export class SalaryPaymentDto extends BaseEntityDto {
    employeeName: string | null;
    employeeId: number;
    basicSalary : number;
    tax : number;
    netAmount:number;
    date: Date;
    total:number;
    leave:number;
    deduction:number;
    lastName:string|null;
    allReadyPaid:string|null; 
}
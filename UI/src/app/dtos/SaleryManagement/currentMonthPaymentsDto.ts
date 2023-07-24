export class CurrentMonthPaymentsDto {

    lastName: string| null ;
    employeeName: string | null;
    employeeId: number | null;
    basicSalary: number | null;
    tax: number | null;
    leaveCount: number | null;
    deduction: number | null;
    isPaid: boolean;
    netAmount: number | null;
    date: Date;
    leaveType:string|null;
   
}
export class CurrentMonthPaymentsDto {
    employeeName: string | null;
    employeeId: number | null;
    basicSalary: number | null;
    tax: number | null;
    leaveCount: number | null;
    paidLeaveCount: number | null;
    deduction: number | null;
    isPaid: boolean;
    netAmount: number | null;
    date: Date;
    leaveType:string|null;
}
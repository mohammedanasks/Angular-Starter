export class GetEmploysForPaymentDto {
    employeeName: string | null;
    departmentName: string | null;
    designationName: string | null;
    lastName: string | null;
    employeeId: number | null;
    basicSalary: number | null;
    tax: number | null;
    leaveCount: number | null;
    paidLeaveCount: number | null;
    deduction: number | null;
    isPaid: boolean;
    leaveType: string | null;
    netAmount: number | null;
    date: Date;
}
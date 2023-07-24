export class DepartmentReportDto {
    employeeName: string | null;
    departmentName: string | null;
    employeeId: number;
    leaveCount: number;
    leaveTypeId: number;
    leaveTypeName: string | null;
    fromDate: string;
    toDate: string;
    totalSalary:number;
    totalTax:number;

}
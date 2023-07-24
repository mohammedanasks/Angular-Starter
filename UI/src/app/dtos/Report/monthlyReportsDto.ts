export interface MonthlyReportDto {
    month: string | null;
    date: string;
    employeeId: number | null;
    totalSalary: number | null;
    tax: number | null;
    deduction: number | null;
    leave: number;
}
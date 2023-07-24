import { BaseEntityDto } from "../base/baseEntityDto";

export class EmployeeDto extends BaseEntityDto {
    firstName: string | null;
    lastName: string | null;
    fileName: string | null;
    departmentName: string | null;
    departmentId: number | null;
    designationName: string | null;
    designationId: number | null;
    contact: number | null;
    joinDate: string;
    basicSalary: number | null;
    tax: number | null;
    totalWorkingDays: number | null;
    leave: number | null;
    receivedSalary: number | null;
    salaryTotal: number | null;
    firstNameRequired: string | null;
    lastNameRequired: string | null;
    designationRequired: string | null;
    departmentNameRequired: string | null;
    contactRequired: string | null;
    salaryRequired: string | null;
}
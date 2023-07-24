import { BaseEntityDto } from "../base/baseEntityDto";

export class LeaveDto extends BaseEntityDto {
    employeeName: string | null;
    departmentName: string | null;
    employeeId: number | null;
    leaveTypeId: number | null;
    leaveTypeName: string | null;
    fromDate: string | null;
    toDate: string | null;
    nameRequired: string | null;
    dateRequired: string | null;
    leaveTypeRequired: string | null;
}
import { BaseEntityDto } from "../base/baseEntityDto";

export class LeaveTypeDto extends BaseEntityDto {
    isPaid: boolean;
    leaveTypeName: string | null;
}
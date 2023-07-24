import { BaseEntityDto } from "../base/baseEntityDto";

export class DepartmentDto extends BaseEntityDto {
    name: string | null;
    departmentHeadName: string | null;
    departmentHeadId: number | null;
    contact: number;
}
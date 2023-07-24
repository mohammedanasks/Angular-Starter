import { BaseEntityDto } from "../base/baseEntityDto";

export class DesignationDto extends BaseEntityDto {
    name: string | null;
    label: string | null;
    description: string | null;
}
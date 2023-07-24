export class ResponseModel<T> {
    item: T | null;
    items: T[] | null;
    isOk: boolean;
    message: string | null;
}
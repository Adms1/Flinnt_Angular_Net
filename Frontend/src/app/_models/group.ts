import { Standard } from "./standard";

export class Group {
    instituteGroupId: number;
    instituteId: number;
    boardId: number;
    mediumId : number;
    standardId : number;
    standardViewModel?: Standard;
    standards?: Standard[];
}
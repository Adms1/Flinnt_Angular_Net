import { Standard } from "./standard";

export class Group {
    instituteGroupId: number;
    instituteId: number;
    groupStructureId: number;
    boardId: number;
    mediumId : number;
    standardId : number;
    standardViewModel?: Standard;
    standards?: Standard[];
}
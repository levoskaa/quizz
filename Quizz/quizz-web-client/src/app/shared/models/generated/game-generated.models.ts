//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.17.0.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

export interface AnswerDto {
  text?: string | undefined;
  isCorrect?: boolean;
  displayIndex?: number;
  correctIndex?: number;
}

export interface AnswerViewModel {
  text: string;
  isCorrect?: boolean | undefined;
  correctIndex?: number | undefined;
  displayIndex?: number | undefined;
}

export interface CreateGameDto {
  name: string;
}

export interface ErrorViewModel {
  message?: string | undefined;
  errors?: string[] | undefined;
  stackTrace?: string | undefined;
}

export interface GameViewModel {
  id: number;
  name: string;
  updatedAt: Date;
}

export interface GameViewModelPaginatedItemsViewModel {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: GameViewModel[];
}

export interface Int32EntityCreatedViewModel {
  id: number;
}

export interface ProblemDetails {
  type?: string | undefined;
  title?: string | undefined;
  status?: number | undefined;
  detail?: string | undefined;
  instance?: string | undefined;

  [key: string]: any;
}

export interface QuestionDto {
  text?: string | undefined;
  type?: QuestionType;
  index?: number;
  timeLimitInSeconds?: number;
  correctAnswer?: boolean;
  answerPossibilities?: AnswerDto[] | undefined;
}

export enum QuestionType {
  TrueOrFalse = 'TrueOrFalse',
  MultipleChoice = 'MultipleChoice',
  FindOrder = 'FindOrder',
  FreeText = 'FreeText',
}

export interface QuestionViewModel {
  text: string;
  type: QuestionType;
  index: number;
  timeLimitInSeconds: number;
  correctAnswer?: boolean | undefined;
  answerPossibilities?: AnswerViewModel[] | undefined;
}

export interface QuestionsListViewModel {
  data: QuestionViewModel[];
}

export interface UpdateGameDto {
  name: string;
}

export interface UpdateGameQuestionsDto {
  questions?: QuestionDto[] | undefined;
}

import { QuestionViewModel } from 'src/app/shared/models/generated/game-generated.models';

export class UpdateInviteCode {
  static readonly type = '[Quiz Runner] Update Invite Code';
  constructor(public inviteCode: string) {}
}

export class QuestionReceived {
  static readonly type = '[Quiz Runner] Question Received';
  constructor(public question: QuestionViewModel) {}
}

export class UpdateQuestionCount {
  static readonly type = '[Quiz Runner] Update Question count';
  constructor(public count: number) {}
}

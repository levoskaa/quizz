import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext, StateToken } from '@ngxs/store';
import { QuestionViewModel } from 'src/app/shared/models/generated/game-generated.models';
import {
  QuestionReceived,
  UpdateInviteCode,
  UpdateQuestionCount,
} from '../actions/quiz-runner.actions';

const QUIZ_RUNNER_STATE_TOKEN = new StateToken<QuizRunnerStateModel>('quizRunner');

interface QuizRunnerStateModel {
  inviteCode?: string;
  question?: QuestionViewModel;
  questionCount: number;
}

const defaults: QuizRunnerStateModel = {
  inviteCode: undefined,
  question: undefined,
  questionCount: 0,
};

@State<QuizRunnerStateModel>({
  name: QUIZ_RUNNER_STATE_TOKEN,
  defaults,
})
@Injectable()
export class QuizRunnerState {
  @Selector()
  static inviteCode(state: QuizRunnerStateModel): string | undefined {
    return state.inviteCode;
  }

  @Selector()
  static currentQuestion(state: QuizRunnerStateModel): QuestionViewModel | undefined {
    return state.question;
  }

  @Selector()
  static questionCount(state: QuizRunnerStateModel): number {
    return state.questionCount;
  }

  @Action(UpdateInviteCode)
  updateInviteCode(ctx: StateContext<QuizRunnerStateModel>, action: UpdateInviteCode) {
    ctx.patchState({
      inviteCode: action.inviteCode,
    });
  }

  @Action(QuestionReceived)
  questionReceived(ctx: StateContext<QuizRunnerStateModel>, action: QuestionReceived) {
    ctx.patchState({
      question: action.question,
    });
  }

  @Action(UpdateQuestionCount)
  updateQuestionCount(ctx: StateContext<QuizRunnerStateModel>, action: UpdateQuestionCount) {
    ctx.patchState({
      questionCount: action.count,
    });
  }
}

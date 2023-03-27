import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext, StateToken } from '@ngxs/store';
import { QuestionViewModel } from 'src/app/shared/models/generated/game-generated.models';
import { QuestionReceived, UpdateInviteCode } from '../actions/quiz-runner.actions';

const QUIZ_RUNNER_STATE_TOKEN = new StateToken<QuizRunnerStateModel>('quizRunner');

interface QuizRunnerStateModel {
  inviteCode?: string;
  question?: QuestionViewModel;
  players: string[];
}

@State<QuizRunnerStateModel>({
  name: QUIZ_RUNNER_STATE_TOKEN,
  defaults: {
    inviteCode: undefined,
    question: undefined,
    players: [],
  },
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

  @Action(UpdateInviteCode)
  inviteCodeReceived(ctx: StateContext<QuizRunnerStateModel>, action: UpdateInviteCode) {
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
}

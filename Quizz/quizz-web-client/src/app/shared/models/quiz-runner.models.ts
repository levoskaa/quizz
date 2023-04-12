export interface ParticipantResultViewModel {
  name: string;
  connectionId: string;
  score: number;
}

export interface QuizResultsViewModel {
  participantResults: ParticipantResultViewModel[];
}

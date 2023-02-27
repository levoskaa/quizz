export interface DialogConfig<T = any> {
  autoFocus?: boolean;
  data?: T;
}

export interface ConfirmDialogData {
  title: string;
  text: string;
  isDelete: boolean;
}

export enum DialogCloseType {
  Successful = 'Successful',
  Canceled = 'Canceled',
}

export interface DialogResult<T = any> {
  closeType: DialogCloseType;
  data: T;
}

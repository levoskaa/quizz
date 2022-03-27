export interface DialogConfig {
  autoFocus?: boolean;
  data?: any;
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

export interface DialogResult {
  closeType: DialogCloseType;
  data: any;
}

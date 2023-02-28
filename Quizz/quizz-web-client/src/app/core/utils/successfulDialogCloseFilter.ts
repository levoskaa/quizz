import { filter } from 'rxjs/operators';
import { DialogCloseType, DialogResult } from 'src/app/shared/models/dialog.models';

export function successfulDialogCloseFilter() {
  return filter(
    (result: DialogResult) => result?.closeType && result.closeType === DialogCloseType.Successful
  );
}

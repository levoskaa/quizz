import { HttpParams } from '@angular/common/http';

/* eslint-disable @typescript-eslint/no-explicit-any */
export const objectToHttpParams = (object: any): HttpParams | undefined => {
  if (object === null || object === undefined) {
    return;
  }
  for (const key of Object.keys(object)) {
    if (object[key] === undefined || object[key] === null) {
      delete object[key];
    }
  }
  return new HttpParams({ fromObject: object });
};

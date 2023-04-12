import { Injectable, OnDestroy } from '@angular/core';
import { Observable, PartialObserver, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export abstract class UnsubscribeOnDestroy implements OnDestroy {
  protected subscriptions: Subscription[] = [];

  protected subscribe<T>(observable: Observable<T>, observer?: PartialObserver<T>): void {
    const subscription = observable.subscribe(observer);
    this.subscriptions.push(subscription);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }
}

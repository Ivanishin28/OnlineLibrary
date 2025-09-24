import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { switchMap, take } from 'rxjs';

export const tokenAppendingInterceptor: HttpInterceptorFn = (req, next) => {
  const authCredentials = inject(AuthService);

  return authCredentials.token$.pipe(
    take(1),
    switchMap((token) => {
      let authReq = req;
      if (token) {
        authReq = req.clone({
          setHeaders: {
            Authorization: `Bearer ${token.value}`,
          },
        });
      }

      return next(authReq);
    })
  );
};

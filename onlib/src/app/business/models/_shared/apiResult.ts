import { ApiError } from './apiError';

export interface ApiResult<T> {
  data: T;
  errors: ApiError[];
}

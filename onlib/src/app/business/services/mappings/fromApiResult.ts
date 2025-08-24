import { ApiResult } from '../../models/_shared/apiResult';
import { Result } from '../../models/_shared/result';

export function valueFromApiResult<T>(apiResult: ApiResult<T>): T {
  return apiResult.data;
}

export function resultFromApiResult<T>(apiResult: ApiResult<T>): Result<T> {
  if (apiResult.errors && apiResult.errors.length > 0) {
    return Result.success(apiResult.data);
  } else {
    return Result.failure(apiResult.errors);
  }
}

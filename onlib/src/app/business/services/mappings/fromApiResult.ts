import { ApiError } from '../../models/_shared/apiError';
import { ApiResult } from '../../models/_shared/apiResult';
import { BusinessError } from '../../models/_shared/businessError';
import { Result } from '../../models/_shared/result';

export function valueFromApiResult<T>(apiResult: ApiResult<T>): T {
  return apiResult.data;
}

export function resultFromApiResult<T>(apiResult: ApiResult<T>): Result<T> {
  console.log(apiResult, !apiResult.errors || apiResult.errors.length == 0);
  if (!apiResult.errors || apiResult.errors.length == 0) {
    return Result.success(apiResult.data);
  } else {
    return Result.failure(
      apiResult.errors.map((apiError) => errorFromApiError(apiError))
    );
  }
}

export function errorFromApiError<T>(apiError: ApiError): BusinessError {
  return new BusinessError(apiError.code, apiError.message);
}

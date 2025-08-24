import { ApiResult } from '../../models/_shared/apiResult';

export function fromApiResult<T>(apiResult: ApiResult<T>): T {
  return apiResult.data;
}

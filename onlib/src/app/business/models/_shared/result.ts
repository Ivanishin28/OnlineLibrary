import { BusinessError } from './businessError';

export class Result<T> {
  private constructor(
    private readonly _value?: T,
    private readonly _errors: BusinessError[] = []
  ) {}

  public get value(): T {
    if (!this.isSuccess) {
      throw new Error('Cannot access value of a failed result.');
    }
    return this._value!;
  }

  public get errors(): BusinessError[] {
    return [...this._errors];
  }

  public get errorMessage(): string {
    return this._errors.map((x) => x.message).join(' ');
  }

  public get isSuccess(): boolean {
    return this._errors.length === 0;
  }

  public toFailure<T>(): Result<T> {
    if (this.isSuccess) {
      throw new Error('Success to Failure cast');
    }

    return Result.failure<T>(this.errors);
  }

  public static success<T>(value: T): Result<T> {
    return new Result(value);
  }

  public static failure<T>(errors: BusinessError[]): Result<T> {
    return new Result<T>(undefined, errors);
  }
}

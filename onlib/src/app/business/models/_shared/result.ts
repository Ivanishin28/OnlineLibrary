export class Result<T> {
  private constructor(
    private readonly _value?: T,
    private readonly _errors: string[] = []
  ) {}

  public get value(): T {
    if (!this.isSuccess) {
      throw new Error('Cannot access value of a failed result.');
    }
    return this._value!;
  }

  public get errors(): string[] {
    return this._errors;
  }

  public get errorMessage(): string {
    return this._errors.join(' ');
  }

  public get isSuccess(): boolean {
    return this._errors.length === 0;
  }

  public static success<T>(value: T): Result<T> {
    return new Result(value);
  }

  public static failure<T>(errors: string[]): Result<T> {
    return new Result<T>(undefined, errors);
  }
}

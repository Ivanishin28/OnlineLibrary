export class Pagination<T> {
  constructor(public readonly total: number, public readonly items: T[]) {}
}

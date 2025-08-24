export class CreateBookRequest {
  constructor(public title: string, public author_ids: string[] = []) {}
}

import { IdentityId } from './identityId';
import { UserId } from './userId';

export class UserCredentials {
  constructor(
    public readonly identityId: IdentityId,
    public readonly userId: UserId
  ) {}
}

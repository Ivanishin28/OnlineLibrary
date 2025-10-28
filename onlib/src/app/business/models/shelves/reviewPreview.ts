import { IdentityPreview } from '../identity/identityPreview';

export interface ReviewPreview {
  id: string;
  user_id: string;
  book_id: string;
  rating: number;
  text: string | null;
  identity: IdentityPreview;
}

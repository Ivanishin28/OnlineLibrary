import { DateOnly } from '../../types/dateOnly';

export interface FullAuthor {
  id: string;
  creator_id: string;
  first_name: string;
  last_name: string;
  middle_name: string | undefined;
  birth_date: DateOnly;
  avatar_id: string | undefined;
  biography: string | undefined;
}


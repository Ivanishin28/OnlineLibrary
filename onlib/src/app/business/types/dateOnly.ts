export type DateOnly = string & { __brand: 'DateOnly' };

export const toDate = (value: DateOnly): Date => {
  return new Date(`${value}T00:00:00`);
};

export interface Pagination {
  pageNumber: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
}

export const DefaultPagination: Pagination = {
  pageNumber: 1,
  pageSize: 10,
  totalItems: 0,
  totalPages: 0,
};

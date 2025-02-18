import { PaginatedListOfBoatDto } from "../api-client/web-api-client.ts";
import { DefaultPagination, Pagination } from "../interfaces/pagination.ts";

export const getPaginationFromPaginatedListOfBoatDto = (
  boats: PaginatedListOfBoatDto | null
): Pagination => {
  if (!boats) return DefaultPagination;
  return {
    pageNumber: boats.pageNumber ?? DefaultPagination.pageNumber,
    pageSize: boats.pageSize ?? DefaultPagination.pageSize,
    totalItems: boats.totalCount ?? DefaultPagination.totalItems,
    totalPages: boats.totalPages ?? DefaultPagination.totalPages,
  };
};

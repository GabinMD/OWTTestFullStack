import React from "react";
import { Pagination } from "../interfaces/pagination.ts";

interface PaginationProps {
  pagination: Pagination;
  onPageChange: (newPagination: Pagination) => void;
}

const PaginationComponent = ({ pagination, onPageChange }: PaginationProps) => {
  const handlePageSizeChange = (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    onPageChange({
      ...pagination,
      pageNumber: 1,
      pageSize: parseInt(event.target.value),
    });
  };

  return (
    <div className="flex flex-col sm:flex-row items-center justify-center gap-4 mt-6">
      <div className="flex items-center gap-2">
        <span className="text-sm text-white-700">Afficher</span>
        <select
          className="border border-gray-300 rounded-md px-2 py-1 text-sm"
          value={pagination.pageSize}
          onChange={handlePageSizeChange}
        >
          {[5, 10, 20, 50].map((size) => (
            <option key={size} value={size}>
              {size}
            </option>
          ))}
        </select>
        <span className="text-sm text-white-700">par page</span>
      </div>

      <div className="flex items-center gap-2">
        <button
          className={`px-3 py-1 rounded-md ${
            pagination.pageNumber === 1
              ? "bg-gray-300 cursor-not-allowed"
              : "bg-blue-500 hover:bg-blue-600 text-white"
          }`}
          onClick={() =>
            onPageChange({
              ...pagination,
              pageNumber: pagination.pageNumber - 1,
            })
          }
          disabled={pagination.pageNumber === 1}
        >
          ← Précédent
        </button>
        {Array.from(
          { length: pagination.totalPages },
          (_, index) => index + 1
        ).map((page) => (
          <button
            key={page}
            className={`px-3 py-1 rounded-md ${
              pagination.pageNumber === page
                ? "bg-blue-700 text-white"
                : "bg-gray-200 hover:bg-gray-300"
            }`}
            onClick={() =>
              onPageChange({
                ...pagination,
                pageNumber: page,
              })
            }
          >
            {page}
          </button>
        ))}
        <button
          className={`px-3 py-1 rounded-md ${
            pagination.pageNumber === pagination.totalPages
              ? "bg-gray-300 cursor-not-allowed"
              : "bg-blue-500 hover:bg-blue-600 text-white"
          }`}
          onClick={() =>
            onPageChange({
              ...pagination,
              pageNumber: pagination.pageNumber + 1,
            })
          }
          disabled={pagination.pageNumber === pagination.totalPages}
        >
          Suivant →
        </button>
      </div>
    </div>
  );
};

export default PaginationComponent;

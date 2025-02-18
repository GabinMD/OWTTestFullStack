import { useEffect, useState } from "react";
import BoatCard from "./BoatCard";
import CreateBoatModal from "./modals/CreateBoatModal.tsx";
import ConfirmModal from "./modals/ConfirmModal.tsx";
import PaginationComponent from "../PaginationComponent.tsx";
import { getPaginationFromPaginatedListOfBoatDto } from "../../utils/boats-utils.ts";
import { Pagination } from "../../interfaces/pagination.ts";
import { PaginatedListOfBoatDto } from "../../api-client/web-api-client.ts";
import ErrorListComponent from "./ErrorsListComponent.tsx";

interface BoatListProps {
  boats: PaginatedListOfBoatDto | null;
  errors: string[];
  fetchBoats: (pagination: Pagination) => Promise<void>;
  createBoat: (name: string, description: string) => Promise<void>;
  deleteBoat: (id: number) => Promise<void>;
  purgeBoats: () => Promise<void>;
  isLoading: boolean;
}

const BoatList = (props: BoatListProps) => {
  const {
    boats,
    errors,
    fetchBoats: refreshBoats,
    createBoat,
    deleteBoat,
    purgeBoats,
    isLoading,
  } = props;

  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showConfirmDelete, setShowConfirmDelete] = useState<number | null>(
    null
  );
  const [showConfirmPurge, setShowConfirmPurge] = useState(false);

  const [pagination, setPagination] = useState<Pagination>(
    getPaginationFromPaginatedListOfBoatDto(boats)
  );

  const handlePageChange = (pagination: Pagination) => {
    setPagination(pagination);
    refreshBoats(pagination);
  };

  useEffect(() => {
    const newPagination = getPaginationFromPaginatedListOfBoatDto(boats);
    if (
      newPagination.pageNumber !== pagination.pageNumber ||
      newPagination.pageSize !== pagination.pageSize ||
      newPagination.totalItems !== pagination.totalItems ||
      newPagination.totalPages !== pagination.totalPages
    ) {
      setPagination(newPagination);
    }
  }, [boats]);

  return (
    <div>
      <ErrorListComponent errors={errors} />
      <div className="flex justify-between mb-4">
        <button
          className="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600 transition"
          onClick={() => setShowCreateModal(true)}
        >
          + Créer un bateau
        </button>
        <button
          className="bg-red-700 text-white px-4 py-2 rounded-md hover:bg-red-800 transition"
          onClick={() => setShowConfirmPurge(true)}
        >
          ⚠️ Purge Boats
        </button>
      </div>

      {isLoading && <p className="text-gray-500">Chargement des bateaux...</p>}

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {boats?.items?.map((boat) => (
          <BoatCard
            key={boat.id}
            boat={boat}
            onDelete={() => setShowConfirmDelete(boat.id ?? -1)}
          />
        ))}
      </div>

      <PaginationComponent
        pagination={pagination}
        onPageChange={handlePageChange}
      />

      {showCreateModal && (
        <CreateBoatModal
          onClose={() => setShowCreateModal(false)}
          onCreate={(name, description) => {
            createBoat(name, description).then(() => {
              setShowCreateModal(false);
            });
          }}
        />
      )}

      {showConfirmDelete !== null && (
        <ConfirmModal
          message="Voulez-vous vraiment supprimer ce bateau ?"
          onConfirm={() => {
            deleteBoat(showConfirmDelete);
            setShowConfirmDelete(null);
          }}
          onCancel={() => setShowConfirmDelete(null)}
        />
      )}

      {showConfirmPurge && (
        <ConfirmModal
          message="🚨 Voulez-vous vraiment supprimer TOUS les bateaux ? Cette action est irréversible."
          onConfirm={() => {
            purgeBoats();
            setShowConfirmPurge(false);
          }}
          onCancel={() => setShowConfirmPurge(false)}
        />
      )}
    </div>
  );
};

export default BoatList;

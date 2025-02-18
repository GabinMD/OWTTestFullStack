import { useState } from "react";
import { useBoats } from "../../hooks/boats/useBoats";
import BoatCard from "./BoatCard";
import CreateBoatModal from "./modals/CreateBoatModal.tsx";
import ConfirmModal from "./modals/ConfirmModal.tsx";

const BoatList = () => {
  const { boats, createBoat, deleteBoat, purgeBoats, isLoading } = useBoats();
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showConfirmDelete, setShowConfirmDelete] = useState<number | null>(
    null
  );
  const [showConfirmPurge, setShowConfirmPurge] = useState(false);

  return (
    <div>
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

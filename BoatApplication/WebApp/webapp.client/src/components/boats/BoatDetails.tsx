import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ConfirmModal from "./modals/ConfirmModal";
import { BoatDto } from "../../api-client/web-api-client.ts";

interface BoatDetailsProps {
  boat: BoatDto | null;
  updateBoat: (id: number, name: string, description: string) => void;
  deleteBoat: (id: number) => void;
  isLoading: boolean;
}

const BoatDetails = (props: BoatDetailsProps) => {
  const { boat, updateBoat, deleteBoat, isLoading } = props;

  const navigate = useNavigate();

  const [isEditing, setIsEditing] = useState(false);
  const [boatData, setBoatData] = useState({ name: "", description: "" });
  const [showConfirmDelete, setShowConfirmDelete] = useState(false);

  // 🔥 Mettre à jour les champs si boat change
  useEffect(() => {
    if (boat) {
      setBoatData({
        name: boat.name ?? "",
        description: boat.description ?? "",
      });
    }
  }, [boat]);

  const handleSave = () => {
    if (boat?.id) updateBoat(boat.id, boatData.name, boatData.description);
    setIsEditing(false);
  };

  return (
    <div className="container mx-auto px-6 pt-20">
      <div className="bg-white shadow-md rounded-lg p-6 max-w-2xl mx-auto">
        {isLoading ? (
          <p className="text-gray-500 text-center">Chargement du bateau...</p>
        ) : (
          <>
            <h1 className="text-3xl font-bold text-blue-700 text-center mb-4">
              {isEditing ? "Modifier le bateau" : boat?.name}
            </h1>

            {isEditing ? (
              <div className="flex flex-col gap-4">
                <input
                  className="w-full p-2 border border-gray-300 rounded-md"
                  placeholder="Nom"
                  value={boatData.name}
                  onChange={(e) =>
                    setBoatData({ ...boatData, name: e.target.value })
                  }
                />
                <textarea
                  className="w-full p-2 border border-gray-300 rounded-md"
                  placeholder="Description"
                  value={boatData.description}
                  onChange={(e) =>
                    setBoatData({ ...boatData, description: e.target.value })
                  }
                />
                <div className="flex justify-end gap-2">
                  <button
                    className="px-3 py-1 bg-gray-300 rounded-md"
                    onClick={() => setIsEditing(false)}
                  >
                    Annuler
                  </button>
                  <button
                    className="px-3 py-1 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition"
                    onClick={handleSave}
                  >
                    Sauvegarder
                  </button>
                </div>
              </div>
            ) : (
              <div className="text-gray-600 text-lg">
                <p className="mb-4">
                  <strong>Description :</strong> {boat?.description}
                </p>
                <div className="flex justify-between mt-4">
                  <button
                    className="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600 transition"
                    onClick={() => navigate("/boats")}
                  >
                    🔙 Retour à la liste
                  </button>
                  <div className="flex gap-2">
                    <button
                      className="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600 transition"
                      onClick={() => setIsEditing(true)}
                    >
                      ✏️ Modifier
                    </button>
                    <button
                      className="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-600 transition"
                      onClick={() => setShowConfirmDelete(true)}
                    >
                      🗑️ Supprimer
                    </button>
                  </div>
                </div>
              </div>
            )}
          </>
        )}
      </div>

      {showConfirmDelete && (
        <ConfirmModal
          message="Êtes-vous sûr de vouloir supprimer ce bateau ?"
          onConfirm={() => {
            deleteBoat(boat?.id ?? -1);
            navigate("/boats");
          }}
          onCancel={() => setShowConfirmDelete(false)}
        />
      )}
    </div>
  );
};

export default BoatDetails;

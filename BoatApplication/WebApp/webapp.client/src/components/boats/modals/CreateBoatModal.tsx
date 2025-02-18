import { useState } from "react";

interface CreateBoatModalProps {
  onClose: () => void;
  onCreate: (name: string, description: string) => void;
}

const CreateBoatModal = ({ onClose, onCreate }: CreateBoatModalProps) => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
      <div className="bg-white p-6 rounded-lg shadow-lg w-96">
        <h2 className="text-xl font-bold mb-4 text-black">Créer un bateau</h2>
        <input
          className="w-full p-2 border border-gray-300 rounded-md mb-3 text-black"
          placeholder="Nom"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <input
          className="w-full p-2 border border-gray-300 rounded-md mb-3 text-black"
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <div className="flex justify-end gap-2">
          <button
            className="px-3 py-1 bg-gray-300 rounded-md"
            onClick={onClose}
          >
            Annuler
          </button>
          <button
            className="px-3 py-1 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition"
            onClick={() => onCreate(name, description)}
          >
            Créer
          </button>
        </div>
      </div>
    </div>
  );
};

export default CreateBoatModal;

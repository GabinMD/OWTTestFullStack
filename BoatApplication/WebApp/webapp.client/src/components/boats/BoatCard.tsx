import { useNavigate } from "react-router-dom";
import { BoatDto } from "../../api-client/web-api-client.ts";

interface BoatCardProps {
  boat: BoatDto;
  onDelete: (id: number) => void;
}

const BoatCard = ({ boat, onDelete }: BoatCardProps) => {
  const navigate = useNavigate();

  return (
    <div className="bg-white p-4 rounded-lg shadow-md hover:shadow-lg transition">
      <h2 className="text-xl font-semibold text-blue-700">{boat.name}</h2>
      <p className="text-gray-600">Description : {boat.description}</p>
      <div className="flex justify-between mt-3">
        <button
          onClick={() => navigate(`/boats/${boat.id}`)}
          className="bg-green-500 text-white px-3 py-1 rounded-md hover:bg-green-600 transition"
        >
          Éditer
        </button>
        <button
          onClick={() => onDelete(boat.id ?? -1)}
          className="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-600 transition"
        >
          Supprimer
        </button>
      </div>
    </div>
  );
};

export default BoatCard;

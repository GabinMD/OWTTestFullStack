import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { useBoats } from "../../hooks/boats/useBoats";
import BoatDetails from "../../components/boats/BoatDetails.tsx";

const Boat = () => {
  const { id } = useParams();
  const { currentBoat, fetchSingleBoat, updateBoat, deleteBoat, isLoading } =
    useBoats();

  // 🔥 Charger le bateau au montage
  useEffect(() => {
    if (id) fetchSingleBoat(Number(id));
  }, [id]);

  return (
    <BoatDetails
      boat={currentBoat}
      updateBoat={updateBoat}
      deleteBoat={deleteBoat}
      isLoading={isLoading}
    />
  );
};

export default Boat;

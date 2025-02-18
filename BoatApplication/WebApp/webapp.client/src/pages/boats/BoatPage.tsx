import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { useBoats } from "../../hooks/boats/useBoats";
import BoatDetails from "../../components/boats/BoatDetails.tsx";

const Boat = () => {
  const { id } = useParams();
  const {
    currentBoat,
    errors,
    fetchSingleBoat,
    updateBoat,
    deleteBoat,
    isLoading,
  } = useBoats();

  // 🔥 Charger le bateau au montage
  useEffect(() => {
    if (id) fetchSingleBoat(Number(id));
  }, [id]);

  return (
    <div className="container mx-auto px-6 pt-20  w-screen">
      <BoatDetails
        boat={currentBoat}
        errors={errors}
        updateBoat={updateBoat}
        deleteBoat={deleteBoat}
        isLoading={isLoading}
      />
    </div>
  );
};

export default Boat;

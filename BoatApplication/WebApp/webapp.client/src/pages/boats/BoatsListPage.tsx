import { useEffect } from "react";
import BoatList from "../../components/boats/BoatList";
import { useBoats } from "../../hooks/boats/useBoats.ts";
import { DefaultPagination } from "../../interfaces/pagination.ts";

const BoatListPage = () => {
  const {
    boats,
    errors,
    fetchAllBoats,
    createBoat,
    deleteBoat,
    purgeBoats,
    isLoading,
  } = useBoats();

  useEffect(() => {
    fetchAllBoats(DefaultPagination, true);
  }, []);

  return (
    <div className="container mx-auto px-6 pt-20  w-screen">
      <BoatList
        boats={boats}
        errors={errors}
        fetchBoats={(pagination) => fetchAllBoats(pagination, true)}
        createBoat={createBoat}
        deleteBoat={deleteBoat}
        purgeBoats={purgeBoats}
        isLoading={isLoading}
      />
    </div>
  );
};

export default BoatListPage;

import { useEffect } from "react";
import BoatList from "../../components/boats/BoatList";
import { useBoats } from "../../hooks/boats/useBoats.ts";
import { DefaultPagination } from "../../interfaces/pagination.ts";

const BoatListPage = () => {
  const { fetchAllBoats } = useBoats();

  useEffect(() => {
    fetchAllBoats(DefaultPagination, true);
  }, []);

  return (
    <div className="container mx-auto px-6 pt-20  w-screen">
      <BoatList />
    </div>
  );
};

export default BoatListPage;

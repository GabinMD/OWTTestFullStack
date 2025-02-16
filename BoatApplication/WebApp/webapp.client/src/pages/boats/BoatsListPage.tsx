import { useAuth } from "../../hooks/identity/useAuth.ts";

const BoatListPage = () => {
  const { user, token } = useAuth();
  return (
    <div>
      <h2>Liste des Bateaux</h2>
      {token}
      <ul>
        <li>Id: {user?.id}</li>
        <li>Name: {user?.name}</li>
      </ul>
    </div>
  );
};

export default BoatListPage;

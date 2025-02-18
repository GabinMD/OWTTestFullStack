import { useAuth } from "../hooks/identity/useAuth";
import { useNavigate } from "react-router-dom";

const Navbar = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <nav className="bg-gradient-to-r from-blue-700 to-blue-500 text-white shadow-md fixed top-0 left-0 w-full flex items-center justify-between px-6 py-4">
      <div
        className="text-xl font-bold tracking-wide cursor-pointer hover:opacity-80"
        onClick={() => navigate("/")}
      >
        🚤 Boat Manager
      </div>

      <div className="flex-1"></div>

      <div className="flex items-center gap-6">
        {user && (
          <div className="hidden sm:flex flex-col items-end bg-white/20 px-3 py-1 rounded-md">
            <span className="text-sm font-medium">👤 {user.name}</span>
            <span className="text-xs text-white/80">ID: {user.id}</span>
          </div>
        )}
        <button
          onClick={handleLogout}
          className="bg-red-600 px-4 py-2 rounded-lg text-sm font-semibold shadow-md hover:bg-red-700 transition"
        >
          Logout
        </button>
      </div>
    </nav>
  );
};

export default Navbar;

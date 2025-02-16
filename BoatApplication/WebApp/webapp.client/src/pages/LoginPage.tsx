import { useState } from "react";
import { useAuth } from "../hooks/identity/useAuth";
import { useNavigate } from "react-router-dom";

const LoginPage = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isRegisterMode, setIsRegisterMode] = useState(false);

  const { user, login, register, errors } = useAuth();
  const navigate = useNavigate();

  const handleAuth = async () => {
    try {
      if (isRegisterMode) {
        await register(username, password);
      } else {
        await login(username, password);
      }
      if (user) navigate("/boats");
    } catch (error) {
      console.error("Erreur lors de l'authentification:", error);
    }
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen w-screen bg-gray-100">
      <div className="w-full max-w-md p-6 bg-white shadow-md rounded-lg">
        <h2 className="text-2xl font-semibold text-center mb-4 text-gray-900">
          {isRegisterMode ? "Créer un compte" : "Connexion"}
        </h2>

        <input
          className="w-full p-2 border border-gray-300 rounded-md mb-3 text-gray-900"
          placeholder="Nom d'utilisateur"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <input
          className="w-full p-2 border border-gray-300 rounded-md mb-3 text-gray-900"
          placeholder="Mot de passe"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <button
          className="w-full p-2 bg-blue-600 text-white rounded-md 
             hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-300 transition"
          onClick={handleAuth}
        >
          {isRegisterMode ? "S'inscrire" : "Se connecter"}
        </button>

        {errors.map((error, index) => {
          return (
            <p key={index} className="text-red-500 text-sm text-center mt-2">
              {error}
            </p>
          );
        })}

        <p
          onClick={() => setIsRegisterMode(!isRegisterMode)}
          className="mt-3 text-blue-500 text-sm cursor-pointer text-center hover:underline"
        >
          {isRegisterMode
            ? "Déjà un compte ? Se connecter"
            : "Pas de compte ? Créer un compte"}
        </p>
      </div>
    </div>
  );
};

export default LoginPage;

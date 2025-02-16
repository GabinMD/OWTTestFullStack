import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import { useAuth } from "./hooks/identity/useAuth";
import LoginPage from "./pages/LoginPage";
import BoatListPage from "./pages/boats/BoatsListPage";
import PrivateRoute from "./components/PrivateRoute";
import { useEffect } from "react";

const App = () => {
  const { token, initialized, initAuth } = useAuth();

  useEffect(() => {
    if (!initialized) initAuth();
  }, [initialized, initAuth]);

  return (
    <Router>
      <Routes>
        <Route
          path="/login"
          element={token ? <Navigate to="/boats" /> : <LoginPage />}
        />

        <Route
          path="/boats"
          element={
            <PrivateRoute>
              <BoatListPage />
            </PrivateRoute>
          }
        />

        <Route
          path="*"
          element={<Navigate to={token ? "/boats" : "/login"} />}
        />
      </Routes>
    </Router>
  );
};

export default App;

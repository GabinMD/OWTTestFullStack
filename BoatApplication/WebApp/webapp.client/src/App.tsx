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
import Navbar from "./components/NavBar.tsx";
import BoatPage from "./pages/boats/BoatPage.tsx";

const App = () => {
  const { token, initialized, initAuth } = useAuth();

  useEffect(() => {
    if (!initialized) initAuth();
  }, [initialized, initAuth]);

  return (
    <Router>
      {token && <Navbar />}
      <div className="pt-16">
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
            path="/boats/:id"
            element={
              <PrivateRoute>
                <BoatPage />
              </PrivateRoute>
            }
          />

          <Route
            path="*"
            element={<Navigate to={token ? "/boats" : "/login"} />}
          />
        </Routes>
      </div>
    </Router>
  );
};

export default App;

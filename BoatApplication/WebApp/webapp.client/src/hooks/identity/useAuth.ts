import { create } from "zustand";
import { AuthResponse } from "../../api-client/web-api-client";
import {
  fetchLogin,
  fetchRefreshToken,
  fetchRegister,
} from "../../api-client/auth/auth-client.ts";
import { parseJwt } from "../../utils/auth-utils.ts";
import { setBoatClientToken } from "../../api-client/boats/boats-client.ts";
import { User } from "../../interfaces/user.ts";

interface AuthState {
  token: string | null;
  user: User | null;
  errors: string[];
  isLoading: boolean;
  initialized: boolean;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
  register: (email: string, password: string) => Promise<void>;
  refreshToken: () => Promise<void>;
  initAuth: () => Promise<void>;
}

const handleAuthResponse = (response: AuthResponse | undefined) => {
  if (!response) return { errors: ["Erreur lors de la connexion"] };

  localStorage.setItem("token", response.token ?? "");
  localStorage.setItem("refreshToken", response.refreshToken ?? "");
  return {
    token: response.token,
    user: parseJwt(response.token ?? ""),
    errors: response.errors
      ? response.errors.map((e) => e.description ?? "").filter((e) => e)
      : [],
  };
};

export const useAuth = create<AuthState>((set, get) => ({
  token: null,
  user: null,
  errors: [],
  isLoading: true,
  initialized: false,

  login: async (email, password) => {
    set({ isLoading: true });
    const response = await fetchLogin(email, password);
    set(handleAuthResponse(response));
    setBoatClientToken(get().token);
    set({ isLoading: false });
  },

  logout: () => {
    localStorage.removeItem("token");
    localStorage.removeItem("refreshToken");
    set({ token: null, user: null });
  },

  register: async (username, password) => {
    set({ isLoading: true });
    const response = await fetchRegister(username, password);
    set(handleAuthResponse(response));
    setBoatClientToken(get().token);
    set({ isLoading: false });
  },

  refreshToken: async () => {
    set({ isLoading: true });
    const refreshToken = localStorage.getItem("refreshToken");
    if (!refreshToken) return;

    const response = await fetchRefreshToken(refreshToken);
    set(handleAuthResponse(response));
    setBoatClientToken(get().token);
    set({ isLoading: false });
  },

  initAuth: async () => {
    set({ isLoading: true });

    const token = localStorage.getItem("token");
    const user = parseJwt(token);

    if (token && !user) {
      console.log("Token expiré, tentative de refresh...");
      await useAuth.getState().refreshToken();
    } else {
      set({ token, user });
      setBoatClientToken(get().token);
    }

    set({ isLoading: false, initialized: true });
  },
}));

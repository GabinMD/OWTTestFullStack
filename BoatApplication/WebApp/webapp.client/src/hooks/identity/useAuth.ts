import { create } from "zustand";
import {
  fetchLogin,
  fetchRefreshToken,
  fetchRegister,
} from "../../api-client/auth/auth-client.ts";
import { parseJwt } from "../../utils/auth-utils.ts";
import { setBoatClientToken } from "../../api-client/boats/boats-client.ts";
import {
  handleAuthResponse,
  scheduleTokenRefresh,
} from "./helpers/use-auth-helpers.ts";
import { AuthState } from "./interfaces/auth-state.ts";

export const useAuth = create<AuthState>((set, get) => ({
  token: null,
  user: null,
  errors: [],
  isLoading: true,
  initialized: false,
  refreshTokenTimeout: null,

  login: async (email, password) => {
    set({ isLoading: true });
    const response = await fetchLogin(email, password);
    set(handleAuthResponse(response));
    setBoatClientToken(get().token);
    set({
      isLoading: false,
      refreshTokenTimeout: scheduleTokenRefresh(
        get().refreshTokenTimeout,
        get().user?.exp ?? 0,
        get().refreshToken
      ),
    });
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
    set({
      isLoading: false,
      refreshTokenTimeout: scheduleTokenRefresh(
        get().refreshTokenTimeout,
        get().user?.exp ?? 0,
        get().refreshToken
      ),
    });
  },

  initAuth: async () => {
    set({ isLoading: true });

    const token = localStorage.getItem("token");
    const user = parseJwt(token);

    if (token && !user) {
      await useAuth.getState().refreshToken();
    } else {
      set({ token, user });
      setBoatClientToken(get().token);
    }

    set({ isLoading: false, initialized: true });
  },
}));

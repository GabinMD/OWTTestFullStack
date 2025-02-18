import { User } from "../../../interfaces/user.ts";

export interface AuthState {
  token: string | null;
  user: User | null;
  errors: string[];
  isLoading: boolean;
  initialized: boolean;
  refreshTokenTimeout: NodeJS.Timeout | null;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
  register: (email: string, password: string) => Promise<void>;
  refreshToken: () => Promise<void>;
  initAuth: () => Promise<void>;
}

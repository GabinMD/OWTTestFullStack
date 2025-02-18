import { User } from "../interfaces/user.ts";

export const parseJwt = (token: string | null): User | null => {
  if (!token) return null;
  try {
    const payload = JSON.parse(atob(token.split(".")[1]));
    if (payload.exp * 1000 < Date.now()) {
      return null;
    }
    return {
      id: payload.sub,
      name: payload.name,
    };
  } catch (e) {
    console.error("Erreur lors du parsing du token:", e);
    return null;
  }
};

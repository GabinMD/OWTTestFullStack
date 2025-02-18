import { AuthResponse } from "../../../api-client/web-api-client.ts";
import { parseJwt } from "../../../utils/auth-utils.ts";

export const scheduleTokenRefresh = (
  refreshTimeout: NodeJS.Timeout | null,
  expiresIn: number,
  refreshToken: () => void
): NodeJS.Timeout => {
  if (refreshTimeout) clearTimeout(refreshTimeout);

  // Rafraîchir 30 secondes avant expiration
  const refreshTime = Math.max(expiresIn - 30000, 5000);
  refreshTimeout = setTimeout(() => refreshToken(), refreshTime);
  return refreshTimeout;
};

export const handleAuthResponse = (response: AuthResponse | undefined) => {
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

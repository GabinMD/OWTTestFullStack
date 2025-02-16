export const parseJwt = (token: string | null) => {
  if (!token) return null;
  try {
    console.log("parseJwt:", token);
    const payload = JSON.parse(atob(token.split(".")[1]));
    if (payload.exp * 1000 < Date.now()) {
      return null; // Token expiré
    }
    return payload;
  } catch (e) {
    console.error("Erreur lors du parsing du token:", e);
    return null;
  }
};

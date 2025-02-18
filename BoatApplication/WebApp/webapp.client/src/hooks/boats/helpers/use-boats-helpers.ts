import {
  BaseAPIResponse,
  GetBoatResponse,
  GetBoatsResponse,
  PurgeBoatResponse,
} from "../../../api-client/web-api-client.ts";
import { BoatsState } from "../interfaces/boats-state.ts";

export const handleResponseErrors = (response: BaseAPIResponse) => {
  return {
    errors: response.errors
      ? response.errors.map((e) => e.description ?? "").filter((e) => e)
      : [],
  };
};

export const handleGetBoatsResponse = (
  response: GetBoatsResponse | undefined
): Partial<BoatsState> => {
  if (!response) return { errors: ["Erreur lors du chargement des bateaux"] };

  return {
    boats: response.boats,
    ...handleResponseErrors(response),
  };
};

export const handleGetBoatResponse = (
  response: GetBoatResponse | undefined
): Partial<BoatsState> => {
  if (!response) return { errors: ["Bateau introuvable"] };

  return {
    currentBoat: response.boat,
    ...handleResponseErrors(response),
  };
};

export const handlePurgeBoatsResponse = (
  response: PurgeBoatResponse | undefined
): Partial<BoatsState> => {
  if (!response) return { errors: ["Erreur lors de la création du bateau"] };

  if (response.purgedBoats === 0) {
    return { errors: ["Aucun bateau à supprimer"] };
  }

  if (response.errors && response.errors?.length > 0) {
    return handleResponseErrors(response);
  }

  return {
    purgeBoatsCount: response.purgedBoats,
    boats: null,
    ...handleResponseErrors(response),
  };
};

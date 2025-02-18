import { Pagination } from "../../interfaces/pagination.ts";
import {
  BaseAPIResponse,
  BoatClient,
  BoatDto,
  CreateBoatCommand,
  GetBoatResponse,
  GetBoatsResponse,
  PurgeBoatResponse,
  SwaggerException,
  UpdateBoatCommand,
} from "../web-api-client.ts";

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

export const boatClient = new BoatClient(BASE_URL);

export const setBoatClientToken = (token: string | null) => {
  boatClient.setToken(token ?? "");
};

export const fetchBoats = async (
  pagination: Pagination
): Promise<GetBoatsResponse | undefined> => {
  try {
    const response = await boatClient.boat_Boats(
      pagination.pageNumber,
      pagination.pageSize
    );
    return response.result;
  } catch (result) {
    if (result instanceof SwaggerException) {
      return result.result;
    }
    return result as GetBoatsResponse;
  }
};

export const fetchBoatById = async (
  id: number
): Promise<GetBoatResponse | undefined> => {
  try {
    const response = await boatClient.boat_Boat(id);
    return response.result;
  } catch (result) {
    if (result instanceof SwaggerException) {
      return result.result;
    }
    return result as GetBoatResponse;
  }
};

export const fetchCreateBoat = async (
  name: string,
  description: string
): Promise<GetBoatResponse | undefined> => {
  const command = new CreateBoatCommand({
    boat: new BoatDto({ name, description }),
  });
  try {
    const response = await boatClient.boat_Create(command);
    return response.result;
  } catch (result) {
    if (result instanceof SwaggerException) {
      return result.result;
    }
    return result as GetBoatResponse;
  }
};

export const fetchUpdateBoat = async (
  id: number,
  name: string,
  description: string
): Promise<GetBoatResponse | undefined> => {
  const command = new UpdateBoatCommand({
    boat: new BoatDto({ id, name, description }),
    id,
  });
  try {
    const response = await boatClient.boat_Update(command);
    return response.result;
  } catch (result) {
    if (result instanceof SwaggerException) {
      return result.result;
    }
    return result as GetBoatResponse;
  }
};

export const fetchDeleteBoat = async (
  id: number
): Promise<BaseAPIResponse | undefined> => {
  try {
    const response = await boatClient.boat_Delete(id);
    return response.result;
  } catch (result) {
    if (result instanceof SwaggerException) {
      return result.result;
    }
    return result as BaseAPIResponse;
  }
};

export const fetchPurgeBoats = async (): Promise<
  PurgeBoatResponse | undefined
> => {
  try {
    const response = await boatClient.boat_Purge();
    return response.result;
  } catch (result) {
    if (result instanceof SwaggerException) {
      return result.result;
    }
    return result as PurgeBoatResponse;
  }
};

import { create } from "zustand";
import {
  fetchBoats,
  fetchBoat,
  fetchCreateBoat,
  fetchUpdateBoat,
  fetchDeleteBoat,
  fetchPurgeBoats,
} from "../../api-client/boats/boats-client.ts";
import { BoatsState } from "./interfaces/boats-state.ts";
import {
  handleGetBoatResponse,
  handleGetBoatsResponse,
} from "./helpers/use-boats-helpers.ts";
import { Pagination } from "../../interfaces/pagination.ts";
import { getPaginationFromPaginatedListOfBoatDto } from "../../utils/boats-utils.ts";

export const useBoats = create<BoatsState>((set, get) => ({
  boats: null,
  currentBoat: null,
  purgeBoatsCount: null,
  isLoading: false,
  errors: [],

  refreshBoats: async () => {
    const pagination = getPaginationFromPaginatedListOfBoatDto(get().boats);
    await get().fetchAllBoats(pagination, false);
  },

  fetchAllBoats: async (
    pagination: Pagination,
    useIsLoading: boolean = true
  ) => {
    if (useIsLoading) set({ isLoading: true });
    const response = await fetchBoats(pagination);
    set(handleGetBoatsResponse(response));
    if (useIsLoading) set({ isLoading: false });
  },

  fetchSingleBoat: async (id) => {
    set({ isLoading: true });
    const response = await fetchBoat(id);
    set(handleGetBoatResponse(response));
    set({ isLoading: false });
  },

  createBoat: async (name, description) => {
    set({ isLoading: true });
    const response = await fetchCreateBoat(name, description);
    set(handleGetBoatResponse(response));
    await get().refreshBoats();
    set({ isLoading: false });
  },

  updateBoat: async (id, name, description) => {
    set({ isLoading: true });
    const response = await fetchUpdateBoat(id, name, description);
    set(handleGetBoatResponse(response));
    await get().refreshBoats();
    set({ isLoading: false });
  },

  deleteBoat: async (id) => {
    set({ isLoading: true });
    const response = await fetchDeleteBoat(id);
    set(handleGetBoatResponse(response));
    await get().refreshBoats();
    set({ isLoading: false });
  },

  purgeBoats: async () => {
    set({ isLoading: true });
    const response = await fetchPurgeBoats();
    set(handleGetBoatResponse(response));
    await get().refreshBoats();
    set({ isLoading: false });
  },
}));

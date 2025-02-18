import {
  BoatDto,
  PaginatedListOfBoatDto,
} from "../../../api-client/web-api-client.ts";
import { Pagination } from "../../../interfaces/pagination.ts";

export interface BoatsState {
  boats: PaginatedListOfBoatDto | null;
  currentBoat: BoatDto | null;
  purgeBoatsCount: number | null;
  isLoading: boolean;
  errors: string[];
  refreshBoats: () => Promise<void>;
  fetchAllBoats: (
    pagination: Pagination,
    useIsLoading: boolean
  ) => Promise<void>;
  fetchSingleBoat: (id: number) => Promise<void>;
  createBoat: (name: string, description: string) => Promise<void>;
  updateBoat: (id: number, name: string, description: string) => Promise<void>;
  deleteBoat: (id: number) => Promise<void>;
  purgeBoats: () => Promise<void>;
}

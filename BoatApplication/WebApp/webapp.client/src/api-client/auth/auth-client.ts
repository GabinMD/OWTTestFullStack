import {
  AuthClient,
  AuthResponse,
  LoginCommand,
  RefreshTokenCommand,
  RegisterUserCommand,
} from "../web-api-client.ts";

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

export const authClient = new AuthClient(BASE_URL);

export const fetchRegister = async (
  name: string,
  password: string
): Promise<AuthResponse | undefined> => {
  const command = new RegisterUserCommand({ name, password });
  try {
    const response = await authClient.auth_Register(command);
    return response.result;
  } catch (result) {
    return result as AuthResponse;
  }
};

export const fetchLogin = async (
  name: string,
  password: string
): Promise<AuthResponse | undefined> => {
  const command = new LoginCommand({ name, password });
  try {
    const response = await authClient.auth_Login(command);
    return response.result;
  } catch (result) {
    return result as AuthResponse;
  }
};

export const fetchRefreshToken = async (
  refreshToken: string
): Promise<AuthResponse | undefined> => {
  const command = new RefreshTokenCommand({ refreshToken });
  try {
    const response = await authClient.auth_RefreshToken(command);
    return response.result;
  } catch (result) {
    return result as AuthResponse;
  }
};

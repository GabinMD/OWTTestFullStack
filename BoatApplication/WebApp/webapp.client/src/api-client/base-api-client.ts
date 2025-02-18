export class BaseApiClient {
  protected jwtToken: string | null = null;
  setToken(token: string) {
    this.jwtToken = token;
  }
  transformOptions(options: RequestInit): Promise<RequestInit> {
    options.headers = {
      ...options.headers,
      Authorization: `Bearer ${this.jwtToken}`,
    };
    return Promise.resolve(options);
  }
}

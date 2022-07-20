export declare class XhrHttpClient extends HttpClient {
    private readonly _logger;

    constructor(logger: ILogger);

    /** @inheritDoc */
    send(request: HttpRequest): Promise<HttpResponse>;
}
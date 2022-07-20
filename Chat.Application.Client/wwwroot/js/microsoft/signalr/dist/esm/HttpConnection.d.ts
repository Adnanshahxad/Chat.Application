/** @private */
export interface INegotiateResponse {
    connectionId?: string;
    connectionToken?: string;
    negotiateVersion?: number;
    availableTransports?: IAvailableTransport[];
    url?: string;
    accessToken?: string;
    error?: string;
}

/** @private */
export interface IAvailableTransport {
    transport: keyof typeof HttpTransportType;
    transferFormats: (keyof typeof TransferFormat)[];
}

/** @private */
export declare class HttpConnection implements IConnection {
    private _connectionState;
    private _connectionStarted;
    private readonly _httpClient;
    private readonly _logger;
    private readonly _options;
    private transport: undefined?;
    private _startInternalPromise: undefined?;
    private _stopPromise: undefined?;
    private _stopPromiseResolver;
    private _stopError: undefined?;
    private _accessTokenFactory: undefined?;
    private _sendQueue: undefined?;
    readonly features: any;
    baseUrl: string;
    connectionId?: string;
    onreceive: ((data: string | ArrayBuffer) => void) | null;
    onclose: ((e?: Error) => void) | null;
    private readonly _negotiateVersion;

    constructor(url: string, options?: IHttpConnectionOptions);

    start(): Promise<void>;

    start(transferFormat: TransferFormat): Promise<void>;

    send(data: string | ArrayBuffer): Promise<void>;

    stop(error?: Error): Promise<void>;

    private _stopInternal;
    private _startInternal;
    private _getNegotiationResponse;
    private _createConnectUrl;
    private _createTransport;
    private _constructTransport;
    private _startTransport;
    private _resolveTransportOrError;
    private _isITransport;
    private _stopConnection;
    private _resolveUrl;
    private _resolveNegotiateUrl;
}

/** @private */
export declare class TransportSendQueue {
    private readonly _transport;
    private _buffer;
    private _sendBufferedData;
    private _executing;
    private _transportResult: undefined?;
    private _sendLoopPromise;

    constructor(_transport: ITransport);

    send(data: string | ArrayBuffer): Promise<void>;

    stop(): Promise<void>;

    private _bufferData;
    private _sendLoop;
    private static _concatBuffers;
}
using BTCPayServer.Client.Models;
using BTCPayServer.Lightning;
using NBitcoin;

namespace BTCPayApp.Core.BTCPayServer;

//methods available on the hub in the client
public interface IBTCPayAppHubClient
{
    Task NotifyServerEvent(ServerEvent ev);
    Task NotifyNetwork(string network);
    Task NotifyServerNode(string nodeInfo);
    Task TransactionDetected(TransactionDetectedRequest request);
    Task NewBlock(string block);
    Task StartListen(string key);

    Task<LightningInvoice> CreateInvoice(string key, CreateLightningInvoiceRequest createLightningInvoiceRequest);
    Task<LightningInvoice?> GetLightningInvoice(string key, uint256 paymentHash);
    Task<LightningPayment?> GetLightningPayment(string key, uint256 paymentHash);
    Task CancelInvoice(string key, uint256 paymentHash);
    Task<List<LightningPayment>> GetLightningPayments(string key, ListPaymentsParams request);
    Task<List<LightningInvoice>> GetLightningInvoices(string key, ListInvoicesParams request);
    Task<PayResponse> PayInvoice(string key, string bolt11, long? amountMilliSatoshi);
    Task MasterUpdated(long? deviceIdentifier);
    Task<LightningNodeInformation> GetLightningNodeInfo(string key);
    Task<LightningNodeBalance> GetLightningBalance(string key);
}

//methods available on the hub in the server
public interface IBTCPayAppHubServer
{
    Task<bool> DeviceMasterSignal(long deviceIdentifier, bool active);
    Task<Dictionary<string,string>> Pair(PairRequest request);
    Task<AppHandshakeResponse> Handshake(AppHandshake request);
    Task<bool> BroadcastTransaction(string tx);
    Task<decimal> GetFeeRate(int blockTarget);
    Task<BestBlockResponse?> GetBestBlock();
    Task<TxInfoResponse> FetchTxsAndTheirBlockHeads(string identifier, string[] txIds, string[] outpoints);
    Task<ScriptResponse> DeriveScript(string identifier);
    Task TrackScripts(string identifier, string[] scripts);
    Task<string> UpdatePsbt(string[] identifiers, string psbt);
    Task<Dictionary<string, CoinResponse[]>> GetUTXOs(string[] identifiers);
    Task<Dictionary<string, TxResp[]>> GetTransactions(string[] identifiers);
    Task SendInvoiceUpdate(LightningInvoice lightningInvoice);
    Task<long?> GetCurrentMaster();
}

public class ServerEvent
{
    public string Type { get; set; } = null!;
    public string? StoreId { get; set; }
    public string? UserId { get; set; }
    public string? AppId { get; set; }
    public string? InvoiceId { get; set; }
    public string? Detail { get; set; }
}

public record TxResp
{
    public string TransactionId { get; set; } = null!;
    public long Confirmations { get; set; }
    public long? Height { get; set; }
    public decimal BalanceChange { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public override string ToString()
    {
        return $"{{ Confirmations = {Confirmations}, Height = {Height}, BalanceChange = {BalanceChange}, Timestamp = {Timestamp}, TransactionId = {TransactionId} }}";
    }
}

public class TransactionDetectedRequest
{
    public string? Identifier { get; set; }
    public string? TxId { get; set; }
    public string[]? SpentScripts { get; set; }
    public string[]? ReceivedScripts { get; set; }
    public bool Confirmed { get; set; }
}

public class CoinResponse
{
    public bool Confirmed { get; set; }
    public string? Script { get; set; }
    public string? Outpoint { get; set; }
    public decimal Value { get; set; }
    public string? Path { get; set; }
}

public class TxInfoResponse
{
    public Dictionary<string,TransactionResponse>? Txs { get; set; }
    public Dictionary<string,string>? BlockHeaders { get; set; }
    public Dictionary<string,int>? BlockHeights { get; set; }
}

public class TransactionResponse
{
    public string? BlockHash { get; set; }
    public string? Transaction { get; set; }
}

public class BestBlockResponse
{
    public string? BlockHash { get; set; }
    public int BlockHeight { get; set; }
    public string? BlockHeader { get; set; }
}

public class ScriptResponse
{
    public string Script { get; set; } = null!;
    public string KeyPath { get; set; } = null!;
}

public class AppHandshake
{
    public string[]? Identifiers { get; set; }
}

//response about identifiers being tracked successfully
public class AppHandshakeResponse
{
    public string[]? IdentifiersAcknowledged { get; set; }
}

public class PairRequest
{
    public Dictionary<string, DerivationItem> Derivations { get; set; } = new();
}

public class DerivationItem
{
    public string? Descriptor { get; set; }
    public int Index { get; set; }
    public OutPoint[] KnownCoins { get; set; } = [];
}

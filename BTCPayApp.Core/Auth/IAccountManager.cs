using BTCPayApp.CommonServer.Models;
using BTCPayApp.Core.Helpers;

namespace BTCPayApp.Core.Auth;

public interface IAccountManager
{
    public BTCPayAccount? GetAccount();
    public Task<IEnumerable<BTCPayAccount>> GetAccounts(string? hostFilter = null);
    public AppUserInfo? GetUserInfo();
    public BTCPayAppClient GetClient(string? baseUri = null);
    public Task<bool> CheckAuthenticated(bool refreshUser = false);
    public Task<bool> IsAuthorized(string policy, object? resource = null);
    public Task<FormResult> Login(string serverUrl, string email, string password, string? otp, CancellationToken? cancellation = default);
    public Task<FormResult> LoginWithCode(string serverUrl, string email, string code, CancellationToken? cancellation = default);
    public Task<FormResult> Register(string serverUrl, string email, string password, CancellationToken? cancellation = default);
    public Task<FormResult> ResetPassword(string serverUrl, string email, string? resetCode, string? newPassword, CancellationToken? cancellation = default);
    public Task<FormResult> SetCurrentStoreId(string storeId);
    public AppUserStoreInfo? GetCurrentStore();
    public AppUserStoreInfo? GetUserStore(string storeId);
    public Task Logout();
    public Task UpdateAccount(BTCPayAccount account);
    public Task RemoveAccount(BTCPayAccount account);
    public AsyncEventHandler<BTCPayAccount?>? OnBeforeAccountChange { get; set; }
    public AsyncEventHandler<BTCPayAccount?>? OnAfterAccountChange { get; set; }
}

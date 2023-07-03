using Web.Hubs;

namespace Web.SignalR;

public static class SignalRConstants
{
    public const string RealTimeProductInfoHubRoute = "/realTimeProductInfoHub";
    public const string SubscribeForProductInfoChanges = nameof(RealTimeProductInfoHub.SubscribeForProductInfoChanges);
    public const string ProductWatchersCountChanged = "ProductWatchersCountChanged";
    public const string ProductOptionQuantityChanged = "ProductOptionQuantityChanged";
    public const string ProductOptionReservedQuantityChanged = "ProductOptionReservedQuantityChanged";
}
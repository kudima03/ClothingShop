namespace Web.SignalR;

public static class SignalRConstants
{
    public const string RealTimeProductInfoHubRoute = "/realTimeProductInfoHub";
    public const string SubscribeForProductInfoChanges = nameof(Hubs.RealTimeProductInfoHub.SubscribeForProductInfoChanges);
    public const string ProductWatchersCountChanged = "ProductWatchersCountChanged";
    public const string ProductOptionQuantityChanged = "ProductOptionQuantityChanged";
    public const string ProductOptionReservedQuantityChanged = "ProductOptionReservedQuantityChanged";
}
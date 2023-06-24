namespace Web.SignalR;

public static class SignalRConstants
{
    public const string OnlineProductViewsHubRoute = "/onlineProductViewsHub";
    public const string GetProductWatchersCount = nameof(Hubs.OnlineProductViewsHub.GetProductWatchersCount);
    public const string ProductWatchersCountChanged = "ProductWatchersCountChanged";
}
using AlcoholGame.Shared.Models;
using AlcoholGame.Shared.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class AlcoholGameServiceCollectionExtensions
{
    /// <summary>Registers the game services used by the shared pages.</summary>
    public static IServiceCollection AddAlcoholGame(this IServiceCollection services)
    {
        services.AddSingleton<DeckService>();
        services.AddSingleton<GameState>();
        return services;
    }
}

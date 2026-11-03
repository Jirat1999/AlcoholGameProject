using AlcoholGame.Shared.Models;
using AlcoholGame.Shared.Services;
using Xunit;

namespace AlcoholGame.Shared.Tests;

public class GameTests
{
    private static readonly Category[] AllCategories = Enum.GetValues<Category>();

    private static GameState NewGame(int seed = 1) =>
        new(new DeckService()) { Rng = new Random(seed) };

    [Fact]
    public void Shuffle_ReturnsEveryCardOfSelectedCategoriesOnce()
    {
        var deck = new DeckService();
        var shuffled = deck.Shuffle([Category.Truth, Category.Dare], new Random(7));

        var expected = deck.All.Count(c => c.Category is Category.Truth or Category.Dare);
        Assert.Equal(expected, shuffled.Count);
        Assert.Equal(shuffled.Count, shuffled.Distinct().Count());
        Assert.All(shuffled, c => Assert.True(c.Category is Category.Truth or Category.Dare));
    }

    [Fact]
    public void EveryCategoryHasCards_AndPlaceholdersAreWellFormed()
    {
        var deck = new DeckService();
        foreach (var cat in AllCategories)
            Assert.Contains(deck.All, c => c.Category == cat);
        Assert.All(deck.All, c => Assert.DoesNotContain("{", c.Text.Replace("{player}", "")));
    }

    [Fact]
    public void Start_RequiresAtLeastTwoDistinctPlayers()
    {
        var g = NewGame();
        Assert.Throws<ArgumentException>(() => g.Start(["A"], AllCategories));
        Assert.Throws<ArgumentException>(() => g.Start(["A", " a ", ""], AllCategories));
        Assert.False(g.Started);
    }

    [Fact]
    public void Start_RejectsTooManyPlayersAndNoCategories()
    {
        var g = NewGame();
        var many = Enumerable.Range(1, GameState.MaxPlayers + 1).Select(i => $"P{i}");
        Assert.Throws<ArgumentException>(() => g.Start(many, AllCategories));
        Assert.Throws<ArgumentException>(() => g.Start(["A", "B"], []));
    }

    [Fact]
    public void Start_TrimsNamesAndBeginsWithFirstPlayer()
    {
        var g = NewGame();
        g.Start(["  Ann ", "Bob"], AllCategories);
        Assert.True(g.Started);
        Assert.Equal(["Ann", "Bob"], g.Players);
        Assert.Equal("Ann", g.CurrentPlayer);
        Assert.NotNull(g.CurrentCard);
        Assert.Equal(1, g.Round);
    }

    [Fact]
    public void NoCardRepeatsWithinARound_AndRoundAdvancesAfterDeckIsUsed()
    {
        var g = NewGame();
        g.Start(["A", "B", "C"], AllCategories);
        var size = g.DeckSize;

        var seen = new List<Card> { g.CurrentCard! };
        for (var i = 1; i < size; i++)
        {
            g.Next();
            seen.Add(g.CurrentCard!);
        }
        Assert.Equal(size, seen.Distinct().Count());
        Assert.Equal(1, g.Round);

        g.Next();
        Assert.Equal(2, g.Round);
        Assert.Equal(1, g.CardsShownThisRound);
    }

    [Fact]
    public void Next_RotatesPlayers()
    {
        var g = NewGame();
        g.Start(["A", "B", "C"], AllCategories);
        var order = new List<string> { g.CurrentPlayer };
        for (var i = 0; i < 3; i++) { g.Next(); order.Add(g.CurrentPlayer); }
        Assert.Equal(["A", "B", "C", "A"], order);
    }

    [Fact]
    public void PlayerPlaceholder_IsReplacedWithADifferentPlayer()
    {
        for (var seed = 0; seed < 20; seed++)
        {
            var g = NewGame(seed);
            g.Start(["A", "B", "C"], AllCategories);
            for (var i = 0; i < g.DeckSize * 2; i++)
            {
                Assert.DoesNotContain("{player}", g.CurrentText);
                if (g.CurrentCard!.Text.Contains("{player}"))
                {
                    // the substituted name must be a player other than the one whose turn it is
                    var rest = g.CurrentCard.Text.Replace("{player}", "\u0000");
                    var idx = rest.IndexOf('\u0000');
                    var others = g.Players.Where(p => p != g.CurrentPlayer);
                    Assert.Contains(others, p => g.CurrentText.Substring(idx).StartsWith(p));
                }
                g.Next();
            }
        }
    }

    [Fact]
    public void Reset_ClearsGame()
    {
        var g = NewGame();
        g.Start(["A", "B"], AllCategories);
        g.Reset();
        Assert.False(g.Started);
        Assert.Null(g.CurrentCard);
        Assert.Empty(g.Players);
        g.Next(); // no-op when not started
        Assert.False(g.Started);
    }
}

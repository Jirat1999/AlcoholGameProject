using AlcoholGame.Shared.Services;

namespace AlcoholGame.Shared.Models;

/// <summary>Session state for one card game. Registered as a singleton so pages share it.</summary>
public sealed class GameState
{
    public const int MinPlayers = 2;
    public const int MaxPlayers = 12;

    private readonly DeckService _decks;
    private List<Card> _deck = [];
    private List<Category> _categories = [];
    private int _index;
    private int _turn;

    public GameState(DeckService decks) => _decks = decks;

    /// <summary>Source of randomness; tests replace it with a seeded instance.</summary>
    public Random Rng { get; set; } = Random.Shared;

    public bool AgeConfirmed { get; set; }
    public IReadOnlyList<string> Players { get; private set; } = [];
    public bool Started { get; private set; }
    public int Round { get; private set; }
    public int CardsShownThisRound => _index;
    public int DeckSize => _deck.Count;

    public Card? CurrentCard { get; private set; }
    public string CurrentText { get; private set; } = "";
    public string CurrentPlayer => Started ? Players[_turn % Players.Count] : "";

    /// <summary>Starts a new game. Names are trimmed, blanks and duplicates (case-insensitive) are dropped.</summary>
    public void Start(IEnumerable<string> players, IEnumerable<Category> categories)
    {
        var names = players
            .Select(p => p.Trim())
            .Where(p => p.Length > 0)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
        if (names.Count < MinPlayers)
            throw new ArgumentException($"ต้องมีผู้เล่นอย่างน้อย {MinPlayers} คน", nameof(players));
        if (names.Count > MaxPlayers)
            throw new ArgumentException($"ผู้เล่นได้สูงสุด {MaxPlayers} คน", nameof(players));

        var cats = categories.Distinct().ToList();
        if (_decks.Shuffle(cats, Rng).Count == 0)
            throw new ArgumentException("ต้องเลือกหมวดการ์ดอย่างน้อย 1 หมวด", nameof(categories));

        Players = names;
        _categories = cats;
        _turn = 0;
        Round = 1;
        Started = true;
        Reshuffle();
        Draw();
    }

    /// <summary>Moves to the next player and the next card; reshuffles when the deck runs out.</summary>
    public void Next()
    {
        if (!Started) return;
        _turn++;
        if (_index >= _deck.Count)
        {
            Round++;
            Reshuffle();
        }
        Draw();
    }

    public void Reset()
    {
        Started = false;
        Players = [];
        _deck = [];
        _index = 0;
        _turn = 0;
        Round = 0;
        CurrentCard = null;
        CurrentText = "";
    }

    private void Reshuffle()
    {
        _deck = _decks.Shuffle(_categories, Rng);
        _index = 0;
    }

    private void Draw()
    {
        var card = _deck[_index++];
        CurrentCard = card;
        CurrentText = card.Text.Contains("{player}")
            ? card.Text.Replace("{player}", PickOtherPlayer())
            : card.Text;
    }

    private string PickOtherPlayer()
    {
        var current = CurrentPlayer;
        var others = Players.Where(p => p != current).ToList();
        return others[Rng.Next(others.Count)];
    }
}

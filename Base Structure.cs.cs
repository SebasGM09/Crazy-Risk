public enum GamePhase
{
    Deployment,
    Attack,
    Fortification,
    Waiting
}

public class Territory
{
    public string Name { get; set; }
    public string Continent { get; set; }
    public Player Owner { get; set; }
    public int Troops { get; set; }
    public List<string> NeighboringTerritories { get; set; }

    public Territory(string name, string continent)
    {
        Name = name;
        Continent = continent;
        Troops = 0;
        NeighboringTerritories = new List<string>();
    }
}

public class Player
{
    public string Name { get; set; }
    public string Color { get; set; }
    public List<Territory> Territories { get; set; }
    public int PendingReinforcements { get; set; }
    public List<Card> Cards { get; set; }

    public Player(string name, string color)
    {
        Name = name;
        Color = color;
        Territories = new List<Territory>();
        Cards = new List<Card>();
        PendingReinforcements = 0;
    }
}

public class Card
{
    public string Territory { get; set; }
    public string Symbol { get; set; } // Infantry, Cavalry, Artillery
}
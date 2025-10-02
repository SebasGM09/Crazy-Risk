public class RiskGame
{
    public List<Player> Players { get; set; }
    public Dictionary<string, Territory> Territories { get; set; }
    public GamePhase CurrentPhase { get; set; }
    public int CurrentPlayerIndex { get; set; }
    public bool AttackPerformed { get; set; }

    public RiskGame()
    {
        Players = new List<Player>();
        Territories = new Dictionary<string, Territory>();
        CurrentPhase = GamePhase.Deployment;
        CurrentPlayerIndex = 0;
        AttackPerformed = false;
        InitializeMap();
    }

    private void InitializeMap()
    {
        // Basic example - expand as needed
        Territories.Add("Alaska", new Territory("Alaska", "North America"));
        Territories.Add("Argentina", new Territory("Argentina", "South America"));
        Territories.Add("Spain", new Territory("Spain", "Europe"));
        // ... add all territories and their neighbors
        
        // Define neighboring territories
        Territories["Alaska"].NeighboringTerritories.Add("Northwest Territory");
        Territories["Spain"].NeighboringTerritories.Add("France");
        // ... continue for all territories
    }
}
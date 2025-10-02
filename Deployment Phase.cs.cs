public class RiskGame
{
    public void CalculateInitialReinforcements(Player player)
    {
        // Basic formula: territories / 3 (minimum 3)
        int reinforcements = Math.Max(player.Territories.Count / 3, 3);
        
        // Continent bonuses
        reinforcements += CalculateContinentBonuses(player);
        
        player.PendingReinforcements = reinforcements;
    }

    private int CalculateContinentBonuses(Player player)
    {
        int bonus = 0;
        var continents = player.Territories.GroupBy(t => t.Continent);
        
        foreach (var continent in continents)
        {
            // Check if player controls the entire continent
            if (Territories.Values
                .Where(t => t.Continent == continent.Key)
                .All(t => t.Owner == player))
            {
                bonus += GetContinentBonus(continent.Key);
            }
        }
        return bonus;
    }

    private int GetContinentBonus(string continent)
    {
        // Standard Risk values
        return continent switch
        {
            "South America" => 2,
            "Africa" => 3,
            "North America" => 5,
            "Europe" => 5,
            "Asia" => 7,
            "Australia" => 2,
            _ => 0
        };
    }

    public bool DeployTroops(Player player, string territory, int amount)
    {
        if (player.PendingReinforcements < amount || amount <= 0)
            return false;

        if (!Territories.ContainsKey(territory) || 
            Territories[territory].Owner != player)
            return false;

        Territories[territory].Troops += amount;
        player.PendingReinforcements -= amount;
        
        // Check if deployment phase ends
        if (player.PendingReinforcements == 0)
        {
            AdvancePhase();
        }
        
        return true;
    }
}
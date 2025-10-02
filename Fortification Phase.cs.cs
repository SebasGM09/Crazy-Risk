public class RiskGame
{
    public bool Fortify(string sourceTerritory, string destinationTerritory, int amount)
    {
        if (!IsValidFortification(sourceTerritory, destinationTerritory, amount))
            return false;

        var source = Territories[sourceTerritory];
        var destination = Territories[destinationTerritory];
        
        source.Troops -= amount;
        destination.Troops += amount;
        
        // Fortification ends the turn
        EndTurn();
        return true;
    }

    private bool IsValidFortification(string source, string destination, int amount)
    {
        if (!Territories.ContainsKey(source) || !Territories.ContainsKey(destination))
            return false;

        var sourceTerritory = Territories[source];
        var destinationTerritory = Territories[destination];
        
        return sourceTerritory.Owner == destinationTerritory.Owner &&
               sourceTerritory.Troops > amount &&
               amount > 0 &&
               AreTerritoriesConnected(source, destination, new HashSet<string>());
    }

    private bool AreTerritoriesConnected(string current, string target, HashSet<string> visited)
    {
        if (current == target) return true;
        
        visited.Add(current);
        foreach (var neighbor in Territories[current].NeighboringTerritories)
        {
            if (!visited.Contains(neighbor) && 
                Territories[neighbor].Owner == Territories[current].Owner)
            {
                if (AreTerritoriesConnected(neighbor, target, visited))
                    return true;
            }
        }
        return false;
    }
}
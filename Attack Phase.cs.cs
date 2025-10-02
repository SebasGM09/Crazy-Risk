public class RiskGame
{
    public AttackResult PerformAttack(string attackingTerritory, string defendingTerritory, int attackingDiceCount)
    {
        // Validations
        if (!IsValidAttack(attackingTerritory, defendingTerritory, attackingDiceCount))
            return new AttackResult { Successful = false };

        var attacker = Territories[attackingTerritory];
        var defender = Territories[defendingTerritory];
        
        // Roll dice
        var attackerDice = RollDice(attackingDiceCount);
        var defenderDice = RollDice(Math.Min(2, defender.Troops)); // Max 2 dice for defender

        // Compare dice results
        var result = CompareDice(attackerDice, defenderDice);
        
        // Apply losses
        attacker.Troops -= result.AttackerLosses;
        defender.Troops -= result.DefenderLosses;
        
        // Check for conquest
        if (defender.Troops == 0)
        {
            ConquerTerritory(defender, attacker.Owner, result.TroopsToMove);
        }

        AttackPerformed = true;
        return result;
    }

    private bool IsValidAttack(string attacker, string defender, int attackingDiceCount)
    {
        if (!Territories.ContainsKey(attacker) || !Territories.ContainsKey(defender))
            return false;

        var attackingTerritory = Territories[attacker];
        var defendingTerritory = Territories[defender];
        
        return attackingTerritory.Owner != defendingTerritory.Owner &&
               attackingTerritory.Troops > attackingDiceCount &&
               attackingDiceCount >= 1 && attackingDiceCount <= 3 &&
               attackingTerritory.NeighboringTerritories.Contains(defender);
    }

    private int[] RollDice(int count)
    {
        Random rnd = new Random();
        var dice = new int[count];
        for (int i = 0; i < count; i++)
        {
            dice[i] = rnd.Next(1, 7);
        }
        return dice.OrderByDescending(d => d).ToArray();
    }

    private AttackResult CompareDice(int[] attackerDice, int[] defenderDice)
    {
        var result = new AttackResult();
        int comparisons = Math.Min(attackerDice.Length, defenderDice.Length);
        
        for (int i = 0; i < comparisons; i++)
        {
            if (attackerDice[i] > defenderDice[i])
                result.DefenderLosses++;
            else
                result.AttackerLosses++; // Defender wins ties
        }
        
        result.TroopsToMove = attackerDice.Length; // Maximum troops to move after conquest
        return result;
    }

    private void ConquerTerritory(Territory territory, Player newOwner, int troopsToMove)
    {
        var originalOwner = territory.Owner;
        
        // Change ownership
        territory.Owner = newOwner;
        originalOwner.Territories.Remove(territory);
        newOwner.Territories.Add(territory);
        
        // Move troops (minimum: dice used in attack)
        int troopsToTransfer = Math.Min(troopsToMove, Territories[territory.Name].Troops - 1);
        territory.Troops = troopsToTransfer;
        
        // Remove troops from attacking territory
        Territories[territory.Name].Troops -= troopsToTransfer;
    }

    public void EndAttackPhase()
    {
        AdvancePhase();
    }
}

public class AttackResult
{
    public bool Successful { get; set; } = true;
    public int AttackerLosses { get; set; }
    public int DefenderLosses { get; set; }
    public int TroopsToMove { get; set; }
    public bool TerritoryConquered { get; set; }
}
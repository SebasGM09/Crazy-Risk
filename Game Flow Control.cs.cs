public class RiskGame
{
    private void AdvancePhase()
    {
        switch (CurrentPhase)
        {
            case GamePhase.Deployment:
                CurrentPhase = GamePhase.Attack;
                AttackPerformed = false;
                break;
            case GamePhase.Attack:
                CurrentPhase = GamePhase.Fortification;
                break;
            case GamePhase.Fortification:
                CurrentPhase = GamePhase.Deployment;
                NextPlayer();
                break;
        }
    }

    private void NextPlayer()
    {
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
        var currentPlayer = Players[CurrentPlayerIndex];
        CalculateInitialReinforcements(currentPlayer);
    }

    private void EndTurn()
    {
        CurrentPhase = GamePhase.Deployment;
        NextPlayer();
    }

    public Player GetCurrentPlayer()
    {
        return Players[CurrentPlayerIndex];
    }

    public bool CheckWinCondition()
    {
        return Players.Any(player => player.Territories.Count == Territories.Count);
    }

    public Player GetWinner()
    {
        return Players.FirstOrDefault(player => player.Territories.Count == Territories.Count);
    }
}
public static class FlavorText
{
    public static string GetFlavorForVikingAction(string vikingName, VikingBoi.Actions action)
    {
        switch (action)
        {

            case VikingBoi.Actions.Attack:
                return vikingName + " attacked or whatever";
            case VikingBoi.Actions.Defend:
            case VikingBoi.Actions.Insult:
            case VikingBoi.Actions.Drink:
            case VikingBoi.Actions.Cower:
            case VikingBoi.Actions.Flee:
            case VikingBoi.Actions.FerociousAttack:
            case VikingBoi.Actions.MockingAttack:
            case VikingBoi.Actions.PlayTheVillain:
            case VikingBoi.Actions.RecklessAttack:
            case VikingBoi.Actions.Plunder:
            case VikingBoi.Actions.DedicateToOdin:
            case VikingBoi.Actions.PrayToTheGods:
            case VikingBoi.Actions.GoBerserk:
            case VikingBoi.Actions.Brag:
            case VikingBoi.Actions.BerserkerRage:       // Group action
            case VikingBoi.Actions.GiveASpeech:         // Group action
            case VikingBoi.Actions.Taunt:               // Group action
            case VikingBoi.Actions.HideInTheBack:       // Group action
            case VikingBoi.Actions.StealAllTheGlory:    // Group action
            case VikingBoi.Actions.GangUp:              // Group action
            default:
                return  vikingName + " ???";
        }
    }


    public static string GetFlavorTextForLairAttack(string vikingName, int damage)
    {
        // WRITE A BUNCH OF ENEMY FLAVOR HERE
        return "A front giant slams " + vikingName + " for " + damage.ToString() + " damage!";
    }

    public static string GetFlavorForTrait(VikingBoi.Traits trait)
    {
        switch(trait)
        {
            case VikingBoi.Traits.Bloodthirsty:
            case VikingBoi.Traits.Greedy:
            case VikingBoi.Traits.Haughty:
            case VikingBoi.Traits.Overconfident:
            case VikingBoi.Traits.Religious:
            case VikingBoi.Traits.Drunkard:
            case VikingBoi.Traits.Cautious:
            case VikingBoi.Traits.Coward:
            case VikingBoi.Traits.Frenzied:
            case VikingBoi.Traits.Suicidal:
            case VikingBoi.Traits.Follower:
            case VikingBoi.Traits.Blowhard:
            case VikingBoi.Traits.Anarchist:
            case VikingBoi.Traits.Jerk:
            case VikingBoi.Traits.Christian:
            case VikingBoi.Traits.PartyAnimal:
            case VikingBoi.Traits.Antagonizer:
            case VikingBoi.Traits.Contrarian:
            case VikingBoi.Traits.Superstitious:
            case VikingBoi.Traits.Violent:
            case VikingBoi.Traits.Performer:
            default:
                return "This is not a trait hehe";
        }
    }
}
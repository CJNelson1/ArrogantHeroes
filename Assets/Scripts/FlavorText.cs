using System;
public static class FlavorText
{
    public static string GetFlavorForVikingAction(string vikingName, VikingBoi.Actions action, int damage)
    {
        switch (action)
        {

            case VikingBoi.Actions.Attack:
                return vikingName + " slices the enemy with their weapon. It doesn't always need to be fancy.";
            case VikingBoi.Actions.Defend:
                return vikingName + " puts up their guard. Its not cowardice if its strategy.";
            case VikingBoi.Actions.Insult:
                return vikingName + " has a thing or two to say about your mother.";
            case VikingBoi.Actions.Drink:
                return vikingName + " grabs some liquid courage. And then some liquid confidence. And some liquid aggression for good measure.";
            case VikingBoi.Actions.Cower:
                return vikingName + " for a brief moment sees the vastness of the universe and their tiny, meaningless place within it. Also they pee themselves.";
            case VikingBoi.Actions.Flee:
                return vikingName + " decides now is a good time to look at different job options far, far away from here.";
            case VikingBoi.Actions.FerociousAttack:
                return vikingName + " attacks so ferociously you might actually think they were good at this.";
            case VikingBoi.Actions.MockingAttack:
                return vikingName + " is toying with the enemy. Sure take your time, its not like your lifes on the the line.";
            case VikingBoi.Actions.PlayTheVillain:
                return vikingName + " threatens their enemy's friends, children, and lovestock. In the Viking world you don't have to be nice to be a hero.";
            case VikingBoi.Actions.RecklessAttack:
                return vikingName + " swings their weapon wildly. It looks cool, but it also leaves them wide open. Thor would be proud.";
            case VikingBoi.Actions.Plunder:
                return vikingName + " just starts stealing stuff. Glory is nice but it can't buy you a new boat.";
            case VikingBoi.Actions.DedicateToOdin:
                return vikingName + " raises their weapon to the sky, letting Odin know this one will be a home run.";
            case VikingBoi.Actions.PrayToTheGods:
                return vikingName + " reminds the gods that they've been a good little Viking all year long.";
            case VikingBoi.Actions.GoBerserk:
                return vikingName + " gives in to their rage. They're a danger to everyone, even themselves.";
            case VikingBoi.Actions.Brag:
                return vikingName + " lets everyone know how great they are. Just awesome, so talented, handsome too.";
            case VikingBoi.Actions.BerserkerRage:       // Group action
                return vikingName + " can't distinguish friend from foe. Might as well kill them all!.";
            case VikingBoi.Actions.GiveASpeech:         // Group action
                return vikingName + " gives a rousing speech to their fellow vikings. Mid-battle. Instead of being helpful.";
            case VikingBoi.Actions.Taunt:               // Group action
                return vikingName + " has the enemy's full attention. That'll happen when you make those gestures with your hands.";
            case VikingBoi.Actions.HideInTheBack:       // Group action
                return vikingName + " is defending their ally's backs. Or hiding behind them, hard to say.";
            case VikingBoi.Actions.StealAllTheGlory:    // Group action
                return vikingName + " really wants the kill credit. \"I did it all by myself!\" they shout to their allies.";
            case VikingBoi.Actions.GangUp:              // Group action
                return vikingName + " realizes theres more than just strength in numbers; theres opportunities for a cheap shot.";
            default:
                return  vikingName + " ???";
        }
    }


    public static string GetFlavorTextForLairAttack(string enemyName, string vikingName, int damage)
    {
        // WRITE A BUNCH OF ENEMY FLAVOR HERE
        Random r = new Random();
        string[] enemyText = new string[7];
        enemyText[0] = " puts the puny mortals in their place.";
        enemyText[1] = " starts a blood feud that will last for generations.";
        enemyText[2] = " grabs a quick bite to eat.";
        enemyText[3] = " uses size to it's advantage.";
        enemyText[4] = " is bigger than you are! Its higher on the food chain!";
        enemyText[5] = " deals a mortal blow. Or a glancing scratch. Couldn't see from here.";
        enemyText[6] = " gives them the ol' one-two, unless its the snake. The snake would probably just bite.";

        return enemyName + enemyText[r.Next(6)];
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
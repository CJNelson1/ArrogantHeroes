using UnityEngine;
using System.Collections.Generic;
using System;

public class Scenario : MonoBehaviour 
{
    private System.Random seed;
    public Dictionary<string, VikingBoi> VikingsInScenario;
    public int scenarioHP;      // Ethan says: I don’t think we should bother giving enemies a defense stat, tankier enemies just have more health
    public bool inCombat;
    public float update;
    private int numberOfVikingsRemaining;
    private bool firstRound;
    public int scenarioLevel;   // 1-5
    public List<String> lastRoundFlavor;

    public void PlayRound()
    {
        // Fuck initiative, just go round robin
        foreach (VikingBoi boi in VikingsInScenario.Values)
        {
            // me_irl
            if (boi.Alive && !boi.stats.ScenarioFled)
            {
                // Reset single round stats for this boi
                boi.stats.ScenarioTakeDoubleDamageNextRound = false;
                boi.stats.ScenarioDeathWard = false;
                boi.stats.ScenarioHasTaunted = false;
                boi.stats.ScenarioIsHiding = false;

                // Lights, camera... 
                VikingAction(boi);
            }

            // Is combat over?
            if (scenarioHP <= 0)
            {
                inCombat = false;
                return;
            }
        }

        // A lich encountered in its lair has a challenge rating of 22
        LairAction();

        // Is combat over?
        if (numberOfVikingsRemaining <= 0)
        {
            inCombat = false;
            return;
        }

        firstRound = false;
    }

    private int damageValue(double damage, double critChance)
    {
        if (seed.NextDouble() < critChance) 
        {
            damage *= 1.5;
        }
        return (int)Math.Round(damage);
    }

    public void VikingAction(VikingBoi boi)
    {
        VikingBoi.Actions selectedAction = boi.SelectAction((numberOfVikingsRemaining > 1), firstRound);
        int temp;

        // Any action that changes stats should change stats for this scenario only - only refer to scenario stats
        switch (selectedAction)
        {
            case VikingBoi.Actions.Attack:
                // Attack the enemy, dealing their attack damage to them
                scenarioHP -= damageValue(boi.stats.ScenarioAttack, boi.stats.ScenarioCritChance);
                break;
            case VikingBoi.Actions.Defend:
                // Add to the vikings defense stat for this scenario only
                // This one has to give diminishing returns so a viking cannot get to 100% defense
                boi.stats.ScenarioDefense = VikingStats.increasePercentageStat(boi.stats.ScenarioDefense);
                break;
            case VikingBoi.Actions.Insult:
                // Talk smack to the enemy, raising the viking's own attack stat
                boi.stats.ScenarioAttack = (int)Math.Round(boi.stats.ScenarioAttack * VikingStats.Modest_Stat_Raise_Multiplier);
                break;
            case VikingBoi.Actions.Drink:
                // Drink some Mead, raising attack stat but lowering defense stat
                boi.stats.ScenarioAttack = (int)Math.Round(boi.stats.ScenarioAttack * VikingStats.Modest_Stat_Raise_Multiplier);
                boi.stats.ScenarioDefense = VikingStats.decreasePercentageStat(boi.stats.ScenarioDefense);
                break;
            case VikingBoi.Actions.Cower:
                // Get scared, do nothing for the round
                break;
            case VikingBoi.Actions.Flee:
                // Run from the scenario. Viking survives scenario but gets no rewards
                boi.stats.ScenarioFled = true;
                numberOfVikingsRemaining--;
                break;
            case VikingBoi.Actions.FerociousAttack:
                // Attack that deals double damage
                scenarioHP -= damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                break;
            case VikingBoi.Actions.MockingAttack:
                // Attack that deals ½ damage. If this attack would bring an enemy to 0 health, it brings them to 1 instead
                scenarioHP -= damageValue(boi.stats.ScenarioAttack * 0.5, boi.stats.ScenarioCritChance);
                if (scenarioHP <= 0) { scenarioHP = 1; }
                break;
            case VikingBoi.Actions.PlayTheVillain:
                // Sneer at the enemy, raise ferocity stat
                boi.stats.ScenarioCritChance = VikingStats.increasePercentageStat(boi.stats.ScenarioCritChance);
                break;
            case VikingBoi.Actions.RecklessAttack:
                // Attack deals double damage, but viking takes double damage next round
                scenarioHP -= damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                boi.stats.ScenarioTakeDoubleDamageNextRound = true;
                break;
            case VikingBoi.Actions.Plunder:
                // Steal instead of attacking. Permanently raise a stat (besides defense) by a small amount
                temp = boi.seed.Next(1, 2);
                if (temp == 1) { boi.stats.ScenarioAttack = (int)Math.Round(boi.stats.ScenarioAttack * VikingStats.Modest_Stat_Raise_Multiplier); }
                else if (temp == 2) { boi.stats.ScenarioCritChance = VikingStats.increasePercentageStat(boi.stats.ScenarioCritChance); }
                break;
            case VikingBoi.Actions.DedicateToOdin:
                // Dedicate the blood shed this day to Odin. Significantly raise ferocity stat
                boi.stats.ScenarioCritChance = (int)Math.Round(boi.stats.ScenarioCritChance * VikingStats.Significant_Stat_Raise_Multiplier);
                break;
            case VikingBoi.Actions.PrayToTheGods:
                // If the Viking’s health would go to 0 this round, it goes to 1 instead
                boi.stats.ScenarioDeathWard = true;
                break;
            case VikingBoi.Actions.GoBerserk:
                // Remove all Defense stat, gain that much in the ferocious stat. Remove all weight from any non-attack action
                boi.stats.ScenarioCritChance += boi.stats.ScenarioDefense;
                if (boi.stats.ScenarioCritChance >= 1) { boi.stats.ScenarioCritChance = 1; }
                boi.stats.ScenarioDefense = 0;
                boi.stats.ScenarioMustAttack = true;
                break;
            case VikingBoi.Actions.Brag:
                // Lowers all stats besides health, raises health
                boi.stats.ScenarioAttack = (int)Math.Round(boi.stats.ScenarioAttack * VikingStats.Modest_Stat_Decrease_Multiplier);
                boi.stats.ScenarioDefense = VikingStats.decreasePercentageStat(boi.stats.ScenarioDefense);
                boi.stats.ScenarioCritChance = VikingStats.decreasePercentageStat(boi.stats.ScenarioCritChance);
                boi.stats.ScenarioHealth += VikingStats.Significant_Health_Raise_Value;
                break;
            case VikingBoi.Actions.BerserkerRage:
                // Ferocious Attack the enemy, but also self
                scenarioHP -= damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                boi.stats.ScenarioHealth -= damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                break;
            case VikingBoi.Actions.GiveASpeech:
                // Raises the attack stat of allies (including self) by a small amount
                foreach (VikingBoi ally in VikingsInScenario.Values)
                {
                    ally.stats.ScenarioAttack = (int)Math.Round(boi.stats.ScenarioAttack * VikingStats.Modest_Stat_Raise_Multiplier);
                }
                break;
            case VikingBoi.Actions.Taunt:
                // Guarantees the enemies next round of attacks will come at this viking in specific
                boi.stats.ScenarioHasTaunted = true;
                break;
            case VikingBoi.Actions.HideInTheBack:
                // Ensures this viking is not an attack option for enemies next round
                boi.stats.ScenarioIsHiding = true;
                break;
            case VikingBoi.Actions.StealAllTheGlory:
                // Deal a ferocious attack. Lower the attack stat of allied vikings (including self)
                scenarioHP -= damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                foreach (VikingBoi ally in VikingsInScenario.Values)
                {
                    ally.stats.ScenarioAttack = (int)Math.Round(boi.stats.ScenarioAttack * VikingStats.Modest_Stat_Decrease_Multiplier);
                }
                break;
            case VikingBoi.Actions.GangUp:
                // Attack, but use the attack stat of whichever viking in the team has the highest attack, rather than your own
                temp = 0;
                foreach (VikingBoi ally in VikingsInScenario.Values)
                {
                    if (ally.stats.ScenarioAttack > temp) { temp = ally.stats.ScenarioAttack; }
                }
                scenarioHP -= damageValue(temp, boi.stats.ScenarioCritChance);
                break;
            default:
                // WTF? Do nothing I guess.
                break;
        }

        this.lastRoundFlavor.Add(FlavorText.GetFlavorForVikingAction(boi.vikingName, selectedAction));
    }

    #region Lair
    public static int calculateDamageOnViking(VikingBoi boi, int rawDamage)
    {
        rawDamage = (int)Math.Round(rawDamage * (1 - boi.stats.ScenarioDefense));
        if (boi.stats.ScenarioTakeDoubleDamageNextRound) { rawDamage *= 2; }
        return rawDamage;
    }

    public void LairAction()
    {
        List<VikingBoi> targetableBois = new List<VikingBoi>();
        VikingBoi tauntingBoi = null;

        foreach (VikingBoi boi in VikingsInScenario.Values)
        {   
            // Get targetable bois
            if (boi.Alive && !boi.stats.ScenarioFled && !boi.stats.ScenarioIsHiding) 
            {
                targetableBois.Add(boi);
            }

            // Get a taunting boi
            if (boi.stats.ScenarioHasTaunted)
            {
                tauntingBoi = boi;
            }
        }

        // Make a couple attacks
        for (int i = 0; i < scenarioLevel; i++)
        {
            if (numberOfVikingsRemaining <= 0)
            {
                return;
            }

            // Deal with a taunting boi
            if (tauntingBoi != null)
            {
                if (tauntingBoi.Alive)
                {
                    tauntingBoi.stats.ScenarioHealth -= calculateDamageOnViking(tauntingBoi, seed.Next(10, 20));   // Do a variable amount of damage
                    if (tauntingBoi.stats.ScenarioHealth <= 0)
                    {
                        if (tauntingBoi.stats.ScenarioDeathWard)
                        {
                            tauntingBoi.stats.ScenarioHealth = 1;
                            this.lastRoundFlavor.Add(tauntingBoi.vikingName + " wards off death!");
                        }
                        else
                        {
                            tauntingBoi.Alive = false;
                            numberOfVikingsRemaining--;
                            targetableBois.Remove(tauntingBoi);
                            this.lastRoundFlavor.Add("The valkyries take " + tauntingBoi.vikingName + " to Valhalla!");
                        }
                    }
                    continue;
                }
            }

            // Get em
            int targetIndex = seed.Next(0, targetableBois.Count);
            VikingBoi targetedBoi = targetableBois[targetIndex];
            targetedBoi.stats.ScenarioHealth -= calculateDamageOnViking(targetedBoi, seed.Next(10, 20));   // Do a variable amount of damage
            if (targetedBoi.stats.ScenarioHealth <= 0)
            {
                if (targetedBoi.stats.ScenarioDeathWard)
                {
                    targetedBoi.stats.ScenarioHealth = 1;
                    this.lastRoundFlavor.Add(targetedBoi.vikingName + " wards off death!");
                }
                else
                {
                    targetedBoi.Alive = false;
                    numberOfVikingsRemaining--;
                    targetableBois.Remove(targetedBoi);
                    this.lastRoundFlavor.Add("The valkyries take " + targetedBoi.vikingName + " to Valhalla!");
                }
            }
        }
    }
    #endregion

    void Start() 
    {
        //VikingsInScenario = roster.SelectedVikingsCopy();     // Roster comes from somewhere?
        numberOfVikingsRemaining = VikingsInScenario.Count;
        firstRound = true;
        inCombat = true;
        this.seed = new System.Random();

        // Set clean temporary stats
        foreach (VikingBoi boi in VikingsInScenario.Values)
        {
            boi.stats.ScenarioHealth = boi.stats.Health;
            boi.stats.ScenarioAttack = boi.stats.Attack;
            boi.stats.ScenarioDefense = boi.stats.Defense;
            boi.stats.ScenarioCritChance = boi.stats.CritChance;
            boi.stats.ScenarioFled = false;
            boi.stats.ScenarioTakeDoubleDamageNextRound = false;
            boi.stats.ScenarioDeathWard = false;
            boi.stats.ScenarioMustAttack = false;
            boi.stats.ScenarioHasTaunted = false;
            boi.stats.ScenarioIsHiding = false;
        }
    }

    void Awake() 
    {
        this.update = 0.0f;    
    }

    void Update() 
    {
        // Every 1 second, update what's going on in the scenario
        this.update += Time.deltaTime;
        if (this.update > 1.0f)
        {
            this.update = 0.0f;
            
            // Play a round if we're in combat
            if (inCombat)
            {
                PlayRound();
            }

            for (int i = 0; i < lastRoundFlavor.Count; i++)
            {
                // PRINT THE SCENE FLAVOR?????????
            }
            lastRoundFlavor.Clear();
        }
    }
}

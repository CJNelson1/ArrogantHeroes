using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scenario : MonoBehaviour 
{
    private System.Random seed;
    public List<VikingBoi> VikingsInScenario;
    public GameObject battleTextholder;
    public GameObject endTextholder;
    public Text battleText;
    public Text endTitle;
    public Text endBody;
    public List<Sprite> enemySprites;
    public GameObject[] vikingSlots;
    public int scenarioHP;      // Ethan says: I don’t think we should bother giving enemies a defense stat, tankier enemies just have more health
    public string enemyName;
    public bool victory;
    public bool inCombat;
    public float update;
    public int roundsCompleted;
    private int numberOfVikingsRemaining;
    private bool firstRound;
    public int scenarioLevel;   // 1-5
    private bool bail = false;
    private bool enemyTurn = false;
    private int whichViking = 0;

    void Update() 
    {
        this.update += Time.deltaTime;
        if (this.update > 4.0f)
        {
            if (bail)
            {
                SceneManager.LoadScene("Map");
            }
            this.update = 0.0f;
            
            // Play a round if we're in combat
            if (inCombat && VikingsInScenario.Count != 0)
            {
                if (enemyTurn)
                {
                    LairRound();
                }
                else
                {
                    vikingTurn();
                }
            }
            else
            {
                ScenarioBail();
            }
        }
    }

    private void vikingTurn()
    {
        VikingBoi boi = VikingsInScenario[whichViking];

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
            victory = true;
        }

        whichViking++;
        if (whichViking >= VikingsInScenario.Count)
        {

            whichViking = 0;
            enemyTurn = true;
        }
    }

    private void LairRound()
    {
        // A lich encountered in its lair has a challenge rating of 22
        if (inCombat)
        {
            LairAction();

            // Is combat over?
            if (numberOfVikingsRemaining <= 0)
            {
                inCombat = false;
                victory = false;
            }
        }
        enemyTurn = false;

        firstRound = false;
        roundsCompleted++;
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
        int damage = 0;

        // Any action that changes stats should change stats for this scenario only - only refer to scenario stats
        switch (selectedAction)
        {
            case VikingBoi.Actions.Attack:
                // Attack the enemy, dealing their attack damage to them
                damage = damageValue(boi.stats.ScenarioAttack, boi.stats.ScenarioCritChance);
                scenarioHP -= damage;
                MusicController.instance.PlaySFX("Slice");                                                          //i'm an sfx
                break;
            case VikingBoi.Actions.Defend:
                // Add to the vikings defense stat for this scenario only
                // This one has to give diminishing returns so a viking cannot get to 100% defense
                boi.stats.ScenarioDefense = VikingStats.increasePercentageStat(boi.stats.ScenarioDefense);
                MusicController.instance.PlaySFX("defend");                                                          //i'm an sfx
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
                MusicController.instance.PlaySFX("fallback");                                                          //i'm an sfx
                break;
            case VikingBoi.Actions.Flee:
                // Run from the scenario. Viking survives scenario but gets no rewards
                boi.stats.ScenarioFled = true;
                numberOfVikingsRemaining--;
                MusicController.instance.PlaySFX("Flee");                                                          //i'm an sfx
                break;
            case VikingBoi.Actions.FerociousAttack:
                // Attack that deals double damage
                damage = damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                scenarioHP -= damage;
                MusicController.instance.PlaySFX("Slam");                                                          //i'm an sfx
                break;
            case VikingBoi.Actions.MockingAttack:
                // Attack that deals ½ damage. If this attack would bring an enemy to 0 health, it brings them to 1 instead
                damage = damageValue(boi.stats.ScenarioAttack * 0.5, boi.stats.ScenarioCritChance);
                scenarioHP -= damage;
                MusicController.instance.PlaySFX("mockingchick");                                                          //i'm an sfx
                if (scenarioHP <= 0) { scenarioHP = 1; }
                break;
            case VikingBoi.Actions.PlayTheVillain:
                // Sneer at the enemy, raise ferocity stat
                boi.stats.ScenarioCritChance = VikingStats.increasePercentageStat(boi.stats.ScenarioCritChance);
                MusicController.instance.PlaySFX("Villain");                                                          //i'm an sfx
                break;
            case VikingBoi.Actions.RecklessAttack:
                // Attack deals double damage, but viking takes double damage next round
                damage = damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                scenarioHP -= damage;
                boi.stats.ScenarioTakeDoubleDamageNextRound = true;
                MusicController.instance.PlaySFX("Slam");                                                          //i'm an sfx
                break;
            case VikingBoi.Actions.Plunder:
                // Steal instead of attacking. Permanently raise a stat (besides defense) by a small amount
                temp = boi.seed.Next(1, 2);
                if (temp == 1) { boi.stats.ScenarioAttack = (int)Math.Round(boi.stats.ScenarioAttack * VikingStats.Modest_Stat_Raise_Multiplier); }
                else if (temp == 2) { boi.stats.ScenarioCritChance = VikingStats.increasePercentageStat(boi.stats.ScenarioCritChance); }
                MusicController.instance.PlaySFX("glasspop");                                                          //i'm an sfx
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
                damage = damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                scenarioHP -= damage;
                boi.stats.ScenarioHealth -= damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                break;
            case VikingBoi.Actions.GiveASpeech:
                // Raises the attack stat of allies (including self) by a small amount
                foreach (VikingBoi ally in VikingsInScenario)
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
                MusicController.instance.PlaySFX("fallback");                                                          //i'm an sfx
                break;
            case VikingBoi.Actions.StealAllTheGlory:
                // Deal a ferocious attack. Lower the attack stat of allied vikings (including self)
                damage = damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                scenarioHP -= damage;
                foreach (VikingBoi ally in VikingsInScenario)
                {
                    ally.stats.ScenarioAttack = (int)Math.Round(boi.stats.ScenarioAttack * VikingStats.Modest_Stat_Decrease_Multiplier);
                }
                break;
            case VikingBoi.Actions.GangUp:
                // Attack, but use the attack stat of whichever viking in the team has the highest attack, rather than your own
                temp = 0;
                foreach (VikingBoi ally in VikingsInScenario)
                {
                    if (ally.stats.ScenarioAttack > temp) { temp = ally.stats.ScenarioAttack; }
                }
                damage = damageValue(boi.stats.ScenarioAttack * 2, boi.stats.ScenarioCritChance);
                scenarioHP -= damage;
                break;
            default:
                // WTF? Do nothing I guess.
                break;
        }

        UpdateTheFlavor(battleText.text = FlavorText.GetFlavorForVikingAction(boi.vikingName, selectedAction, damage));
    }

    #region Lair
    public static int calculateDamageOnViking(VikingBoi boi, int rawDamage)
    {
        if (boi.stats.ScenarioDefense > 0.5) 
        {
            boi.stats.ScenarioDefense = 0.5;
        }
        rawDamage = (int)Math.Round(rawDamage * (1 - boi.stats.ScenarioDefense));
        if (boi.stats.ScenarioTakeDoubleDamageNextRound) { rawDamage *= 2; }
        return rawDamage;
    }

    public void LairAction()
    {
        string flavorYes = "";
        int damage = 0;
        List<VikingBoi> targetableBois = new List<VikingBoi>();
        VikingBoi tauntingBoi = null;

        foreach (VikingBoi boi in VikingsInScenario)
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
                    damage = calculateDamageOnViking(tauntingBoi, seed.Next(10, 20));   // Do a variable amount of damage
                    tauntingBoi.stats.ScenarioHealth -= damage;
                    flavorYes = FlavorText.GetFlavorTextForLairAttack(enemyName, tauntingBoi.vikingName, damage);
                    if (tauntingBoi.stats.ScenarioHealth <= 0)
                    {
                        if (tauntingBoi.stats.ScenarioDeathWard)
                        {
                            tauntingBoi.stats.ScenarioHealth = 1;
                            flavorYes += "\n\n" + tauntingBoi.vikingName + " wards off death!";
                        }
                        else
                        {
                            tauntingBoi.Alive = false;
                            numberOfVikingsRemaining--;
                            vikingSlots[targetableBois.IndexOf(tauntingBoi)].gameObject.SetActive(false);
                            targetableBois.Remove(tauntingBoi);
                            flavorYes += "\n\n" + "The valkyries take " + tauntingBoi.vikingName + " to Valhalla!";
                        }
                    }
                    UpdateTheFlavor(flavorYes);
                    continue;
                }
            }

            if (targetableBois.Count == 0) { return; }

            // Get em
            int targetIndex = seed.Next(0, targetableBois.Count - 1);
            VikingBoi targetedBoi = targetableBois[targetIndex];
            damage = calculateDamageOnViking(targetedBoi, seed.Next(10, 20));   // Do a variable amount of damage
            targetedBoi.stats.ScenarioHealth -= damage;
            flavorYes = FlavorText.GetFlavorTextForLairAttack(enemyName, targetedBoi.vikingName, damage);

            if (targetedBoi.stats.ScenarioHealth <= 0)
            {
                if (targetedBoi.stats.ScenarioDeathWard)
                {
                    targetedBoi.stats.ScenarioHealth = 1;
                    flavorYes += "\n\n" + targetedBoi.vikingName + " wards off death!";
                }
                else
                {
                    targetedBoi.Alive = false;
                    numberOfVikingsRemaining--;
                    targetableBois.Remove(targetedBoi);
                    flavorYes += "\n\n" + "The valkyries take " + targetedBoi.vikingName + " to Valhalla!";
                    vikingSlots[targetIndex].gameObject.SetActive(false);
                }
            }
            UpdateTheFlavor(flavorYes);
        }
    }
    #endregion

    void Start() 
    {
        MusicController.instance.UpdateAudioSources();

        string[] enemies = new string[4]{"Frost Giant", "Kraken", "Troll", "World Eater Snake"};
        int enemyIndex = VikingManager.instance.seed.Next(3);
        
        //Set active vikings in scenario
        VikingsInScenario = new List<VikingBoi>();
        scenarioLevel = ScenarioController.instance.scenarioLevel;
        foreach(VikingBoi v in VikingManager.instance.VMMaster)
        {
            if (v.SelectedForScenario) VikingsInScenario.Add(v);
        }
        
        Sprite chosenSprite = enemySprites[enemyIndex];
        if (scenarioLevel == 5) 
        {
            chosenSprite = enemySprites[3];
            enemyIndex = 3;
        }
        enemyName = enemies[enemyIndex];
        GameObject.Find("Enemy Sprite").GetComponent<Image>().sprite = chosenSprite;
        numberOfVikingsRemaining = VikingsInScenario.Count;
        for(int i = 0; i < 3; i++)
        {
            if(i < numberOfVikingsRemaining)
            {
                GameObject lilvik = vikingSlots[i];
                lilvik.transform.Find("head").GetComponent<Image>().sprite = VikingManager.instance.spriteGenerator.GetHead(VikingsInScenario[i].head);
                lilvik.transform.Find("body").GetComponent<Image>().sprite = VikingManager.instance.spriteGenerator.GetBody(VikingsInScenario[i].body);
                lilvik.transform.Find("legs").GetComponent<Image>().sprite = VikingManager.instance.spriteGenerator.GetLegs(VikingsInScenario[i].legs);
                vikingSlots[i].SetActive(true);
            }
        }
        firstRound = true;
        inCombat = true;
        this.seed = new System.Random();
        scenarioHP = (100 * scenarioLevel);

        // Set clean temporary stats
        foreach (VikingBoi boi in VikingsInScenario)
        {
            boi.Alive = true;
            boi.stats.ScenarioHealth = boi.stats.Health;
            boi.stats.ScenarioAttack = boi.stats.Attack;
            boi.stats.ScenarioDefense = boi.stats.Defense;
            boi.stats.ScenarioCritChance = 0; //boi.stats.CritChance;
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
        this.roundsCompleted = 0;
        battleText.text = "Let the pillaging begin!";
    }

    public void ScenarioBail()
    {
        if (!bail)
        {
            if(victory)
            {
                endTitle.text = "Victory!";
                endBody.text = "Hardly a challenge for such a mighty group of vikings, the enemy is defeated!\n";
                foreach(VikingBoi v in VikingsInScenario)
                {
                    if(!v.Alive)
                    {
                        endBody.text += "\n" + v.vikingName + " is with Odin now.";
                    }
                    else
                    {
                        endBody.text += "\n" + v.vikingName + " leveled up!";
                    }

                    v.LevelUpViking(scenarioLevel);
                }
                if(scenarioLevel >= 5)
                {
                    SceneManager.LoadScene("End Screen");
                }
                
                if (VikingManager.instance.VMMaster.Count < 12) { VikingManager.instance.AddViking(); }
                
            }
            else
            {
                endTitle.text = "Defeat";
                endBody.text = "It is a viking's dream to one day rise the fields of Valhalla. Your vikings have arrived there exceedingly quickly.";
            }
        }
        battleTextholder.SetActive(false);
        endTextholder.SetActive(true);
        bail = true;
    }

    public void UpdateTheFlavor(string flavorToSavor)
    {
        battleText.text = flavorToSavor;
    }
}

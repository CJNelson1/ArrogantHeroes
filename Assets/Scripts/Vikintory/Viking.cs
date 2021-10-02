using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking
{
    public int id;
    public string masterId;
    public string name;
    public int strength;
    public int HP;
    public int maxHP;
    public int head;
    public int body;
    public int legs;

    public Viking(int id, string name, int strength, int HP, int maxHP, string guidId)
    {
        this.id = id;
        this.name = name;
        this.strength = strength;
        this.HP = HP;
        this.maxHP = maxHP;
        this.masterId = guidId;
        this.head = VikingManager.instance.spriteGenerator.AssignHead();
        this.body = VikingManager.instance.spriteGenerator.AssignBody();
        this.legs = VikingManager.instance.spriteGenerator.AssignLegs();
    }
    public Viking(int id, string name, int strength, int HP, int maxHP, string guidId, int head, int body, int legs)
    {
        this.id = id;
        this.name = name;
        this.strength = strength;
        this.HP = HP;
        this.maxHP = maxHP;
        this.masterId = guidId;
        this.head = head;
        this.body = body;
        this.legs = legs;
    }
        public Viking(Viking viking)
    {
        this.id = viking.id;
        this.name = viking.name;
        this.strength = viking.strength;
        this.HP = viking.HP;
        this.maxHP = viking.maxHP;
        this.masterId = viking.masterId;
        this.head = VikingManager.instance.spriteGenerator.AssignHead();
        this.body = VikingManager.instance.spriteGenerator.AssignBody();
        this.legs = VikingManager.instance.spriteGenerator.AssignLegs();
    }
}

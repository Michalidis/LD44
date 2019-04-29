using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public int count;
    public string name;
    public string description;

    public abstract void OnItemPickedUp(GameObject player);
}

public class Item_RingOfHealing : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.Bonus_HealingReceived += 10;
    }
}

public class Item_BootsOfSwiftness : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.Bonus_WalkSpeed += 0.05f;
        stats.RecalculateBaseStats();
    }
}

public class Item_BowOfSpeed : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.Bonus_AttackSpeedPct += 0.15f;
        stats.RecalculateBaseStats();
    }
}

public class Item_HatOfCurse : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        return;
    }
}

public class Item_ShieldOfBlocking : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        return;
    }
}

public class Item_SpearOfBleeding : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        return;
    }
}

public class Item_JacketOfFirepower : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.Bonus_ProjectileSpeed += 0.04f;
        stats.RecalculateBaseStats();
    }
}

public class Item_StoneKey : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.HasStoneKey = true;
        stats.RecalculateBaseStats();
    }
}

public class Item_HelmetOfProtection : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.Bonus_MaxHP += 25;
        stats.RecalculateBaseStats();
        GameObject.FindGameObjectWithTag("UI").GetComponent<Assets.Scripts.UI.UIBehavior>().SetHealth(stats.CurrentHitPoints, stats.MaxHitPoints);
    }
}

public class Item_GoldenKey : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.HasGoldenKey = true;
        stats.RecalculateBaseStats();
    }
}

public class Item_RubyOfFire : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        return;
    }
}

public class Item_SwordOfRapidStrikes : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        return;
    }
}

public class Item_RapierOfDamage : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.Bonus_ProjectileDamagePct += 0.10f;
        stats.RecalculateBaseStats();
    }
}

public class Item_ScepterOfLava : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        return;
    }
}

public class Item_EmblemOfSprint : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.Bonus_RunSpeed += 0.10f;
        stats.RecalculateBaseStats();
    }
}

public class Item_BookOfResurrection : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        return;
    }
}

public class Item_HealthPotion : Item
{
    public override void OnItemPickedUp(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.HealPercentage(Random.Range(20, 35) / 100.0f);
        GameObject.FindGameObjectWithTag("UI").GetComponent<Assets.Scripts.UI.UIBehavior>().SetHealth(stats.CurrentHitPoints, stats.MaxHitPoints);
    }
}

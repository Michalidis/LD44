using Assets.Scripts.Character;
using Assets.Scripts.Monsters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public int count;
    public string name;
    public string description;

    public virtual void OnItemPickedUp(GameObject player) { return; }
    public virtual void OnStruckByEnemy(GameObject player, ref int damage) { return; }
    public virtual void OnEnemyStruck(GameObject player, GameObject enemy) { return; }
    public virtual void OnEnemyKilled(GameObject player, GameObject enemy) { return; }
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
    public override void OnEnemyStruck(GameObject player, GameObject enemy)
    {
        float slow_amount = (count * 0.04f * (2 / (count + 1)));
        player.GetComponent<PlayerStats>().StartCoroutine(SlowEnemy(enemy, slow_amount, 1.5f));
    }

    IEnumerator SlowEnemy(GameObject enemy, float amount, float duration)
    {
        MonsterMovement movement = enemy.GetComponent<MonsterMovement>();
        float final_amount = movement.Speed * amount;
        movement.Speed -= amount;
        yield return new WaitForSeconds(duration);
        movement.Speed += amount;
    }
}


public class Item_ShieldOfBlocking : Item
{
    public override void OnStruckByEnemy(GameObject player, ref int damage)
    {
        // 5% chance to block damage for each shield
        if (count * 5 * (5 / (count + 1)) >= Random.Range(0, 100))
        {
            damage = 0;
        }
    }
}

public class Item_SpearOfBleeding : Item
{
    public override void OnEnemyStruck(GameObject player, GameObject enemy)
    {
        int bleed_amount = count * 4;
        int tick_amount = count + 1;
        if (Random.Range(0, 2) <= 1)
        {
            player.GetComponent<PlayerStats>().StartCoroutine(BleedEnemy(enemy, bleed_amount, tick_amount, 0.5f));
        }
    }

    IEnumerator BleedEnemy(GameObject enemy, int periodic_amount, int tick_amount, float tick_speed)
    {
        Debug.Log(tick_amount + " " + periodic_amount + " " + tick_speed);
        EnemyStats _enemy = enemy.GetComponent<EnemyStats>();
        for (var i = 0; i < tick_amount; i++)
        {
            yield return new WaitForSeconds(tick_speed);
            if (_enemy != null)
            {
                _enemy.TakeDamage(periodic_amount, false);
            }
        }
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
    public override void OnEnemyStruck(GameObject player, GameObject enemy)
    {
        throw new System.NotImplementedException();
    }
}

public class Item_SwordOfRapidStrikes : Item
{
    public override void OnEnemyStruck(GameObject player, GameObject enemy)
    {
        throw new System.NotImplementedException();
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

public class Item_ScepterOfBlood : Item
{
    public override void OnEnemyKilled(GameObject player, GameObject enemy)
    {
        throw new System.NotImplementedException();
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

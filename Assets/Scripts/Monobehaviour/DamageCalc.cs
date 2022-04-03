using System;

public static class DamageCalc
{
    private static Random _random = new Random();

    public static int WeaponDamageCalc(int damage, int critChance)
    {
        return (_random.Next(1, 100) <= critChance) ? damage * 2 : damage;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class Stat 
{
    public int value;
    public float mutation;
    public float scaleFactor;
}

public class CritterStats
{
    public CritterData data;

    public int level;
    public Dictionary<string, Stat> STATS = new Dictionary<string, Stat>()
    {
        {"HP", new Stat {value = 0, mutation = 0, scaleFactor = 1f} },
        {"ATK", new Stat {} },
        {"DEF", new Stat {} },
        {"SATK", new Stat {} },
        {"SDEF", new Stat {} },
        {"SPD", new Stat {} }
    };

    public int XP;
    public int nextLevelXP;

    public int uHP;
    public int uATK;
    public int uDEF;
    public int uSATK;
    public int uSDEF;
    public int uSPD;

    public int currentHP;

    int minUniqueModifier = 1;
    int maxUniqueModifier = 25;

    int baseStatMod = 5;
    int baseXP = 10;

    float mutationFactor = 1.5f;
    float mutationChance = 40f;
    float scaleFactor;

    public CritterStats(CritterData _data, int _level, float _scaleFactor)
    {
        data = _data;
        level = _level;
        scaleFactor = _scaleFactor;
        InitializeStats();
    }

    void InitializeStats()
    {
        XP = Mathf.RoundToInt(baseXP * (Mathf.Pow(level, 3)));
        XP += Mathf.RoundToInt((XP / 6f) * Random.Range(-1f, 1f));
        nextLevelXP = Mathf.RoundToInt(baseXP * (Mathf.Pow(level + 1, 3)));
        nextLevelXP += Mathf.RoundToInt((nextLevelXP / 6f) * Random.Range(-1f, 1f));
        GenerateUniqueValues();
        DeterminMutations();
        SetScaleFactor();
        CalculateStats();
    }

    void DeterminMutations()
    {
        STATS["HP"].mutation = Random.Range(0, 100) < mutationChance ? mutationFactor : 1;
        STATS["ATK"].mutation = Random.Range(0, 100) < mutationChance ? mutationFactor : 1;
        STATS["DEF"].mutation = Random.Range(0, 100) < mutationChance ? mutationFactor : 1;
        STATS["SATK"].mutation = Random.Range(0, 100) < mutationChance ? mutationFactor : 1;
        STATS["SDEF"].mutation = Random.Range(0, 100) < mutationChance ? mutationFactor : 1;
        STATS["SPD"].mutation = Random.Range(0, 100) < mutationChance ? mutationFactor : 1;
    }

    void SetScaleFactor()
    {
        STATS["HP"].scaleFactor = scaleFactor;
        STATS["ATK"].scaleFactor = scaleFactor;
        STATS["DEF"].scaleFactor = scaleFactor;
        if(scaleFactor < 1)
        {
            STATS["SATK"].scaleFactor = Mathf.Abs(1 - scaleFactor) + 1;
            STATS["SDEF"].scaleFactor = Mathf.Abs(1 - scaleFactor) + 1;
            STATS["SPD"].scaleFactor = Mathf.Abs(1 - scaleFactor) + 1;
        }
        else
        {
            STATS["SATK"].scaleFactor = 2 - scaleFactor;
            STATS["SDEF"].scaleFactor = 2 - scaleFactor;
            STATS["SPD"].scaleFactor = 2 - scaleFactor;
        }
    }

    void CalculateStats()
    {
        STATS["HP"].value = Mathf.RoundToInt(((((data.baseHP + uHP) * level) / 10) + baseStatMod) * STATS["HP"].mutation * STATS["HP"].scaleFactor);
        currentHP = STATS["HP"].value;
        STATS["ATK"].value = Mathf.RoundToInt(((((data.baseATK + uATK) * level) / 10) + baseStatMod) * STATS["ATK"].mutation * STATS["ATK"].scaleFactor);
        STATS["DEF"].value = Mathf.RoundToInt(((((data.baseDEF + uDEF) * level) / 10) + baseStatMod) * STATS["DEF"].mutation * STATS["DEF"].scaleFactor);
        STATS["SATK"].value = Mathf.RoundToInt(((((data.baseSATK + uSATK) * level) / 10) + baseStatMod) * STATS["SATK"].mutation * STATS["SATK"].scaleFactor);
        STATS["SDEF"].value = Mathf.RoundToInt(((((data.baseSDEF + uSDEF) * level) / 10) + baseStatMod) * STATS["SDEF"].mutation * STATS["SDEF"].scaleFactor);
        STATS["SPD"].value = Mathf.RoundToInt(((((data.baseSPD + uSPD) * level) / 10) + baseStatMod) * STATS["SPD"].mutation * STATS["SPD"].scaleFactor);
    }

    void GenerateUniqueValues()
    {
        uHP = Random.Range(minUniqueModifier, maxUniqueModifier + 1);
        uATK = Random.Range(minUniqueModifier, maxUniqueModifier + 1);
        uDEF = Random.Range(minUniqueModifier, maxUniqueModifier + 1);
        uSATK = Random.Range(minUniqueModifier, maxUniqueModifier + 1);
        uSDEF = Random.Range(minUniqueModifier, maxUniqueModifier + 1);
        uSPD = Random.Range(minUniqueModifier, maxUniqueModifier + 1);
    }

    public void UpdateHP(int hp)
    {
        currentHP = Mathf.CeilToInt(STATS["HP"].value * (hp / (float)data.health));
    }

    public void AddXP(int xp)
    {
        XP += xp;
        if(XP >= nextLevelXP)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        nextLevelXP = Mathf.RoundToInt(baseXP * (Mathf.Pow(level, 3)));
        nextLevelXP += Mathf.RoundToInt((nextLevelXP / 6f) * Random.Range(-1f, 1f));
        CalculateStats();
    }
}

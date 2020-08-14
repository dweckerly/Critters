using UnityEngine;

public struct Stat 
{
    public int value;
    public int mutation;
    public float scaleFactor;
}

public class CritterStats
{
    public CritterData data;

    public int level;
    public Stat HP;
    public Stat ATK;
    public Stat DEF;
    public Stat SATK;
    public Stat SDEF;
    public Stat SPD;
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
        nextLevelXP = Mathf.RoundToInt(baseXP * (Mathf.Pow(level + 1, 3)));
        GenerateUniqueValues();
        DeterminMutations();
        SetScaleFactor();
        CalculateStats();
    }

    void DeterminMutations()
    {
        HP.mutation = Random.Range(0, 100) < 10 ? 1 : 0;
        ATK.mutation = Random.Range(0, 100) < 10 ? 1 : 0;
        DEF.mutation = Random.Range(0, 100) < 10 ? 1 : 0;
        SATK.mutation = Random.Range(0, 100) < 10 ? 1 : 0;
        SDEF.mutation = Random.Range(0, 100) < 10 ? 1 : 0;
        SPD.mutation = Random.Range(0, 100) < 10 ? 1 : 0;
    }

    void SetScaleFactor()
    {
        HP.scaleFactor = scaleFactor;
        ATK.scaleFactor = scaleFactor;
        DEF.scaleFactor = scaleFactor;
        if(scaleFactor < 1)
        {
            SATK.scaleFactor = Mathf.Abs(1 - scaleFactor) + 1;
            SDEF.scaleFactor = Mathf.Abs(1 - scaleFactor) + 1;
            SPD.scaleFactor = Mathf.Abs(1 - scaleFactor) + 1;
        }
        else
        {
            SATK.scaleFactor = 2 - scaleFactor;
            SDEF.scaleFactor = 2 - scaleFactor;
            SPD.scaleFactor = 2 - scaleFactor;
        }
    }

    void CalculateStats()
    {
        HP.value = Mathf.RoundToInt(((((data.baseHP + uHP) * level) / 10) + baseStatMod) * (mutationFactor * HP.mutation) * HP.scaleFactor);
        currentHP = HP.value;
        ATK.value = Mathf.RoundToInt(((((data.baseATK + uATK) * level) / 10) + baseStatMod) * (mutationFactor * ATK.mutation) * ATK.scaleFactor);
        DEF.value = Mathf.RoundToInt(((((data.baseDEF + uDEF) * level) / 10) + baseStatMod) * (mutationFactor * DEF.mutation) * DEF.scaleFactor);
        SATK.value = Mathf.RoundToInt(((((data.baseSATK + uSATK) * level) / 10) + baseStatMod) * (mutationFactor * SATK.mutation) * SATK.scaleFactor);
        SDEF.value = Mathf.RoundToInt(((((data.baseSDEF + uSDEF) * level) / 10) + baseStatMod) * (mutationFactor * SDEF.mutation) * SDEF.scaleFactor);
        SPD.value = Mathf.RoundToInt(((((data.baseSPD + uSPD) * level) / 10) + baseStatMod) * (mutationFactor * SPD.mutation) * SPD.scaleFactor);
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
        currentHP = Mathf.CeilToInt(HP.value * (hp / (float)data.health));
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
        CalculateStats();
    }
}

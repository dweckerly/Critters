using UnityEngine;

public class CritterStats
{
    public CritterData data;

    public int level;
    public int HP;
    public int ATK;
    public int DEF;
    public int SATK;
    public int SDEF;
    public int SPD;
    public int XP;
    public int nextLevelXP;

    public int uHP;
    public int uATK;
    public int uDEF;
    public int uSATK;
    public int uSDEF;
    public int uSPD;

    int minUniqueModifier = 1;
    int maxUniqueModifier = 15;

    int baseStatMod = 5;
    int baseXP = 10;

    public CritterStats(CritterData _data, int _level)
    {
        data = _data;
        level = _level;
        InitializeStats();
    }

    void InitializeStats()
    {
        XP = Mathf.RoundToInt(baseXP * (Mathf.Pow(level + 1, 3)));
        nextLevelXP = Mathf.RoundToInt(baseXP * (Mathf.Pow(level + 1, 3)));
        GenerateUniqueValues();
        CalculateStats();
    }

    void CalculateStats()
    {
        HP = Mathf.RoundToInt((((data.baseHP + uHP) * level) / 10) + baseStatMod);
        ATK = Mathf.RoundToInt((((data.baseATK + uATK) * level) / 10) + baseStatMod);
        DEF = Mathf.RoundToInt((((data.baseDEF + uDEF) * level) / 10) + baseStatMod);
        SATK = Mathf.RoundToInt((((data.baseSATK + uSATK) * level) / 10) + baseStatMod);
        SDEF = Mathf.RoundToInt((((data.baseSDEF + uSDEF) * level) / 10) + baseStatMod);
        SPD = Mathf.RoundToInt((((data.baseSPD + uSPD) * level) / 10) + baseStatMod);
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

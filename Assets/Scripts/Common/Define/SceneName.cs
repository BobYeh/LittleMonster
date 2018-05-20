using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneName
{
    public static string COMMON = "CommonScene";
    public static string HOME = "HomeScene";
    public static string TITLE = "TitleScene";
    public static string BATTLE_MAP = "BattleMapScene";
    public static string MONSTER_LIST= "MonsterListScene";
    public static string ITEM_LIST= "ItemListScene";
    public static string SHOP = "ShopScene";
    public static string OPTION = "OptionScene";
    public static string[] HOME_GROUP = new string[] { HOME, BATTLE_MAP, MONSTER_LIST, ITEM_LIST, SHOP, OPTION };
    public static string[] TITLE_GROUP = new string[] { TITLE};
}

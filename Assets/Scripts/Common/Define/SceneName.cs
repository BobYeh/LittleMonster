using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneName
{
    public static string COMMON = "CommonScene";
    public static string HOME = "HomeScene";
    public static string TITLE = "TitleScene";
    public static string MONSTER_LIST_SCENE= "MonsterListScene";
    public static string[] HOME_GROUP = new string[] { HOME, MONSTER_LIST_SCENE };
    public static string[] TITLE_GROUP = new string[] { TITLE};
}

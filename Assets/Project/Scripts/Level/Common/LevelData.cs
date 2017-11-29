﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData
{
    public static List<Actor> AllActors = new List<Actor>();
    public static Dictionary<EBattleCamp, List<Actor>> CampActors = new Dictionary<EBattleCamp, List<Actor>>()
    {
        {EBattleCamp.A,new List<Actor>() },
        {EBattleCamp.B,new List<Actor>() },
        {EBattleCamp.C,new List<Actor>() },
        {EBattleCamp.D,new List<Actor>() }
    };
    public static int Chapter;
    public static int SceneID;
    public static float StrTime;
    public static float EndTime;
    public static bool Win;

    public static ActorMainPlayer MainPlayer = null;
    public static ActorPlayer EnemPlayer = null;

    public static List<Actor> GetActorsByActorType(EActorType pType)
    {
        List<Actor> pList = new List<Actor>();
        for (int i = 0; i < AllActors.Count; i++)
        {
            if (AllActors[i].ActorType == pType)
            {
                pList.Add(AllActors[i]);
            }
        }
        return pList;
    }

    public static float CurTime
    {
        get { return Time.realtimeSinceStartup - StrTime; }
    }

    public static Callback Call
    {
        get;
        set;
    }

    public static int Star
    {
        get;
        private set;
    }

    public static bool[] PassContents
    {
        get;
        private set;
    }

    public static void CalcResult()
    {
   
    }

    public static void Reset()
    {  
        Win = false;
        Call = null;
    }

 

  
}
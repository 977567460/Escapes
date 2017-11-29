using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


    public class HolderBorn : LevelContainerBase<LevelBorn>
    {
        public EBattleCamp Camp;

        public override LevelBorn AddElement()
        {
            LevelBorn pBorn = base.AddElement();
            pBorn.Camp = Camp;
            return pBorn;
        }
    }



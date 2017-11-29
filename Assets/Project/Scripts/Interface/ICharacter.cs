using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class ICharacter : IEntiny
{
    public GameObject Obj { get; set; }
    public Transform CacheTransform { get; set; }
    public int GUID { get; set; }
    public int Id { get; set; }


    public ICharacter(int id, int guid)
    {
        this.Id = id;
        this.GUID = guid;
    }
    public abstract void Load(XTransform param);
    public abstract void Init();
    public abstract void Destroy();
    public abstract void Clear();

    public abstract void Step();
    public abstract void ChangeModel(int modelID);
    public abstract bool IsDead();
    public abstract bool IsDestroy();
    public abstract void Pause(bool pause);


}

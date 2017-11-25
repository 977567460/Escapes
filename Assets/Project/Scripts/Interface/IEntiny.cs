using UnityEngine;
using System.Collections;

public interface IEntiny : IObj
{
    int  GUID { get; set; }
    void Destroy();
    void Step();
    void Pause(bool pause);
    bool IsDead();
    bool IsDestroy();
}

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ActorBehavior), false)]
public class InsEntiny : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ActorBehavior pBehavior = target as ActorBehavior;
        if ((pBehavior.Owner is Actor) == false)
        {
            return;
        }
        Actor pActor = pBehavior.Owner as Actor;
        pBehavior.FSM = pActor.FSM;
        pBehavior.Camp = pActor.Camp;

        //if(pActor.GetTarget()!=null)
        //{
        //    pBehavior.Target = pActor.GetTarget().Behavior;
        //}      
   //     serializedObject.FindProperty("Attrs"); 
    }
}

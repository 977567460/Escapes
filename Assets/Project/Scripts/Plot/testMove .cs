using UnityEngine;
using System.Collections;
 
public class testMove : MonoBehaviour
{
 
    void Start ()
    {
      //  CommandManager.Instance.AddCommand (new CommandFadeIn(10));
      //  CommandManager.Instance.AddCommand (new CommandFadeOut(10));
      //  CommandManager.Instance.AddCommand(new CommandAudio("Audios/Event/e01"));
      //  CommandManager.Instance.AddCommand (new CommandMove ("Camera", 54.30874f,47.75602f,34.2717f,"",2.5f));
      //  CommandManager.Instance.AddCommand (new CommandRotate ("Camera", 0, 180, 0, "", 1.5f));
      //  CommandManager.Instance.AddCommand(new CommandDialogEnter());
        ZTPlot.Instance.AddCommand(new CommandDialog("Textues/GUITxetures/Header/portrait00_02", "云天河", "大家好，我就云天河！", 0));
        ZTPlot.Instance.AddCommand(new CommandDialog("Textues/GUITxetures/Header/portrait20_02", "慕容紫英", "云天河！立刻滚到思返谷思过！立刻！", 1));
      //  CommandManager.Instance.AddCommand(new CommandDialogExit());
    }
}


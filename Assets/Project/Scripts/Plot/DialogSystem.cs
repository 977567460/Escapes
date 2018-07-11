using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Assets.Project.Scripts.Plot
{
    public   class DialogSystem : MonoSingleton<DialogSystem>
    {
        public void SetDialog(Dialog dialog, int p)
        {
           UIDialog dialogWnd = (UIDialog)UIManage.Instance.OpenWindow(WindowID.UI_HOME);
           Image header=  LoadResource.Instance.Load<Image>(dialog.Header);
           dialogWnd.showText(dialog.Name, header, dialog.Content);
                 
        }
    }
}

using UnityEngine;
using System.Collections;
using Assets.Project.Scripts.Plot;
 
public class CommandDialog : RPGCommand
{
    /// <summary>
    /// 对话框头像
    /// </summary>
    private string header;
 
    /// <summary>
    /// 对话者姓名
    /// </summary>
    private string name;
 
    /// <summary>
    /// 对话框内容
    /// </summary>
    private string content;
 
    /// <summary>
    /// 对话框索引
    /// </summary>
    private int index;
 
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="header">对话框头像</param>
    /// <param name="name">对话者姓名</param>
    /// <param name="content">对话者内容</param>
    /// <param name="index">对话框索引,0表示左侧对话框,1表示右侧对话框</param>
    public CommandDialog(string header,string name,string content,int index)
    {
        this.header = header;
        this.name = name;
        this.content = content;
        this.index = index;
    }
 
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dialog">对话信息</param>
    /// <param name="index">对话框索引</param>
    public CommandDialog(Dialog dialog,int index)
    {
        this.header = dialog.Header;
        this.name = dialog.Name;
        this.content = dialog.Content;
        this.index = index;
    }
 
    public override void Execute ()
    {
        Dialog dialog = new Dialog();
        dialog.Header = this.header;
        dialog.Name = this.name;
        dialog.Content = this.content;
 
        DialogSystem.Instance.SetDialog(dialog, this.index);
    }
}


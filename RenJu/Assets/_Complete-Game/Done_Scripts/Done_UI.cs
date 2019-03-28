using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_UI : MonoBehaviour {

    public Texture2D blackWin;                   //黑方获胜的贴图
    public Texture2D whiteWin;                   //白方获胜的贴图

    private void OnGUI()
    {
        if (Done_Chess.winner == 1)
        {
            //绘制黑方获胜的图片
            GUI.DrawTexture(new Rect(Screen.width * 0.25f, Screen.height * 0.25f, Screen.width * 0.5f, Screen.height * 0.25f), blackWin);
            //禁用Chess脚本
            FindObjectOfType<Done_Chess>().enabled = false;
        }
        if (Done_Chess.winner == -1)
        {
            //绘制白方获胜的图片
            GUI.DrawTexture(new Rect(Screen.width * 0.25f, Screen.height * 0.25f, Screen.width * 0.5f, Screen.height * 0.25f), whiteWin);
            //禁用Chess脚本
            FindObjectOfType<Done_Chess>().enabled = false;
        }
    }
}

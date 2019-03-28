using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_Restart : MonoBehaviour {

    GameObject[] chess;

   public void restart()
    {
        //获取场景中所有棋子
        chess = GameObject.FindGameObjectsWithTag("chess");
        //销毁棋子
        for(int i = 0; i<chess.Length;i++)
        {
            Destroy(chess[i]);
        }
        //重置棋盘状态
        for(int i = 0;i<15;i++)
        {
            for(int j = 0;j<15;j++)
            {
                Done_Chess.chessState[i, j] = 0;
                
            }
        }
        Done_Chess.winner = 0;
        //启用被UI脚本禁用的Chess脚本
        FindObjectOfType<Done_Chess>().enabled = true;
        //重置落子权限
        Done_Chess.chessTurn = Done_Chess.turn.black;
    }
     

}

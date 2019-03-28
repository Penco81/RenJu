using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_Chess : MonoBehaviour {

    public Transform top_left;           //左上角的空物体
    public Transform top_right;          //右上角的空物体
    public Transform bottom_left;        //左下角的空物体
    public Transform bottom_right;       //右下角的空物体

    Vector2 pos_TL;     //左上角坐标
    Vector2 pos_TR;     //右上角坐标
    Vector2 pos_BL;     //左下角坐标
    Vector2 pos_BR;     //右下角坐标

    float gridWidth;    //棋盘上一格的宽度
    float gridHeight;   //棋盘上一格的高度

    Vector2[,] chessPos;            //可以放置棋子的位置

    Vector2 mousePos;

    public GameObject blackChess;//黑棋预制体
    public GameObject whiteChess;//白棋预制体

    float threshold = 0.4f;      //临界值

    public enum turn
    {
        black,
        white
    }                              //枚举表示黑白双方

    public static turn chessTurn;

    public static int[,] chessState;
    public static int winner;

	// Use this for initialization
	void Start () {
        //初始化可以落子的位置
        chessPos = new Vector2[15, 15];
        //游戏开始时是黑方先下
        chessTurn = turn.black;
        //初始化棋盘状态
        chessState = new int[15, 15];
        
	}
	
	// Update is called once per frame
	void Update () {
        //棋盘左上角位置
        pos_TL = top_left.position;
        //棋盘右上角位置
        pos_TR = top_right.position;
        //棋盘左下角位置
        pos_BL = bottom_left.position;
        //棋盘右下角位置
        pos_BR = bottom_right.position;


        //棋盘上每一格的高度
        gridHeight = (pos_TR.y - pos_BR.y) / 14;
        //棋盘上每一格的宽度
        gridWidth = (pos_TR.x - pos_TL.x) / 14;

        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {     
                //计算棋盘上可以落子的位置
                chessPos[i, j] = new Vector2(pos_TL.x + gridWidth * i, pos_TL.y - gridHeight * j);
                
            }
        }


        if(Input.GetMouseButtonDown(0))
        {
            //记录鼠标点击的坐标并转化为世界坐标
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    //点击位置小于0.4并且点击的位置为空
                    if (Dis(mousePos, chessPos[i, j]) < threshold&&chessState[i,j]==0)
                    {
                        //更新状态，黑子为1，白子为-1
                        chessState[i, j] = chessTurn == turn.black ? 1 : -1;
                        //生成棋子
                        CreateChess(chessPos[i, j]);

                        if(Done_Chess.chessTurn==turn.white)
                        {
                            if (Done_Rule.BanHand1(i, j) || Done_Rule.BanHand2(i, j))
                            {
                                Debug.Log("黑方禁手");
                                winner = -1;
                            }
                        }
                        
                                          
                    }
                }
            }

            //结果判断
            int result = Result();
            if (result == 1)
            {
                Debug.Log("黑棋胜");
                winner = 1;
            }
            if (result == -1)
            {
                Debug.Log("白棋胜");
                winner = -1;
            }
        }

    }

    /// <summary>
    /// 计算鼠标点击的位置与标准位置的坐标之间的距离
    /// </summary>
    float Dis(Vector2 v1, Vector2 v2)
    {
        return Mathf.Sqrt(((v2.x - v1.x) * (v2.x - v1.x)) + ((v2.y - v1.y) * (v2.y - v1.y)));
    }

    void CreateChess(Vector2 v)
    {
        switch(chessTurn)
        {
            //黑方落子
            case turn.black:
                Instantiate(blackChess, v, Quaternion.identity);
                //切换为白方落子
                chessTurn = turn.white;
                break;
            //白方落子
            case turn.white:
                Instantiate(whiteChess, v, Quaternion.identity);
                //切换为黑方落子
                chessTurn = turn.black;
                break;

        }
        
    }

    /// <summary>
    /// 棋子是否连五判断
    /// </summary>
    /// <returns>获胜方，1为黑方，-1为白方</returns>
    int Result()
    {
        int flag = 0;
        switch(chessTurn)
        {

            //进入黑棋是否连五的判断
            case turn.white:
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (j < 4)
                        {

                            //纵向
                            if (chessState[i, j] == 1 && chessState[i, j + 1] == 1 &&
                                chessState[i, j + 2] == 1 && chessState[i, j + 3] == 1 &&
                                chessState[i, j + 4] == 1) 
                            {
                                flag = 1;
                                return flag;
                            }
                            //横向
                            if (chessState[i, j] == 1 && chessState[i + 1, j] == 1 &&
                                chessState[i + 2, j] == 1 && chessState[i + 3, j] == 1 &&
                                chessState[i + 4, j] == 1)
                            {
                                flag = 1;
                                return flag;
                            }
                            //右斜线
                            if (chessState[i, j] == 1 && 
                                chessState[i + 1, j + 1] == 1 &&
                                chessState[i + 2, j + 2] == 1 && 
                                chessState[i + 3, j + 3] == 1 &&
                                chessState[i + 4, j + 4] == 1)
                            {
                                flag = 1;
                                return flag;
                            }
                            //左斜线

                        }
                        else if (j >= 4 && j < 11)
                        {
                            //纵向
                            if (chessState[i, j] == 1 && 
                                chessState[i, j + 1] == 1 &&
                                chessState[i, j + 2] == 1 && 
                                chessState[i, j + 3] == 1 &&
                                chessState[i, j + 4] == 1)
                            {
                               
                                flag = 1;
                                return flag;
                            }
                            //横向
                            if (chessState[i, j] == 1 && 
                                chessState[i + 1, j] == 1 &&
                                chessState[i + 2, j] == 1 && 
                                chessState[i + 3, j] == 1 &&
                                chessState[i + 4, j] == 1)
                            {
                                flag = 1;
                                return flag;
                            }
                            //右斜线
                            if (chessState[i, j] == 1 && 
                                chessState[i + 1, j + 1] == 1 &&
                                chessState[i + 2, j + 2] == 1 && 
                                chessState[i + 3, j + 3] == 1 &&
                                chessState[i + 4, j + 4] == 1)
                            {
                                flag = 1;
                                return flag;
                            }
                            //左斜线
                            if (chessState[i, j] == 1 && 
                                chessState[i + 1, j - 1] == 1 &&
                                chessState[i + 2, j - 2] == 1 && 
                                chessState[i + 3, j - 3] == 1 &&
                                chessState[i + 4, j - 4] == 1)
                            {
                                flag = 1;
                                return flag;
                            }
                        }
                        else
                        {

                            //横向
                            if (chessState[i, j] == 1 && 
                                chessState[i + 1, j] == 1 &&
                                chessState[i + 2, j] == 1 && 
                                chessState[i + 3, j] == 1 &&
                                chessState[i + 4, j] == 1)
                            {
                                flag = 1;
                                return flag;
                            }

                            //左斜线
                            if (chessState[i, j] == 1 && 
                                chessState[i + 1, j - 1] == 1 &&
                                chessState[i + 2, j - 2] == 1 && 
                                chessState[i + 3, j - 3] == 1 &&
                                chessState[i + 4, j - 4] == 1)
                            {
                                flag = 1;
                                return flag;
                            }
                        }

                    }
                }
                for (int i = 11; i < 15; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        //只需要判断纵向   
                        if (chessState[i, j] == 1 && 
                            chessState[i, j + 1] == 1 &&
                            chessState[i, j + 2] == 1 && 
                            chessState[i, j + 3] == 1 &&
                            chessState[i, j + 4] == 1)
                        {
                            flag = 1;
                            return flag;
                        }
                    }
                }
                
              

                break;
            //进入白棋是否连五的判断
            case turn.black:
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (j < 4)
                        {
                            //纵向
                            if (chessState[i, j] == -1 && 
                                chessState[i, j + 1] == -1 &&
                                chessState[i, j + 2] == -1 && 
                                chessState[i, j + 3] == -1 &&
                                chessState[i, j + 4] == -1)
                            {
                                flag = -1;
                                return flag;
                            }
                            //横向
                            if (chessState[i, j] == -1 && 
                                chessState[i + 1, j] == -1 &&
                                chessState[i + 2, j] == -1 && 
                                chessState[i + 3, j] == -1 &&
                                chessState[i + 4, j] == -1)
                            {
                                flag = -1;
                                return flag;
                            }
                            //右斜线
                            if (chessState[i, j] == -1 && 
                                chessState[i + 1, j + 1] == -1 &&
                                chessState[i + 2, j + 2] == -1 && 
                                chessState[i + 3, j + 3] == -1 &&
                                chessState[i + 4, j + 4] == -1)
                            {
                                flag = -1;
                                return flag;
                            }

                        }
                        else if (j >= 4 && j < 11)
                        {
                            //纵向
                            if (chessState[i, j] == -1 && 
                                chessState[i, j + 1] == -1 &&
                                chessState[i, j + 2] == -1 && 
                                chessState[i, j + 3] == -1 &&
                                chessState[i, j + 4] == -1)
                            {
                                flag = -1;
                                return flag;
                            }
                            //横向
                            if (chessState[i, j] == -1 && 
                                chessState[i + 1, j] == -1 &&
                                chessState[i + 2, j] == -1 && 
                                chessState[i + 3, j] == -1 &&
                                chessState[i + 4, j] == -1)
                            {
                                flag = -1;
                                return flag;
                            }
                            //右斜线
                            if (chessState[i, j] == -1 && 
                                chessState[i + 1, j + 1] == -1 &&
                                chessState[i + 2, j + 2] == -1 && 
                                chessState[i + 3, j + 3] == -1 &&
                                chessState[i + 4, j + 4] == -1)
                            {
                                flag = -1;
                                return flag;
                            }
                            //左斜线
                            if (chessState[i, j] == -1 && 
                                chessState[i + 1, j - 1] == -1 &&
                                chessState[i + 2, j - 2] == -1 && 
                                chessState[i + 3, j - 3] == -1 &&
                                chessState[i + 4, j - 4] == -1)
                            {
                                flag = -1;
                                return flag;
                            }
                        }
                        else
                        {

                            //横向
                            if (chessState[i, j] == -1 && 
                                chessState[i + 1, j] == -1 &&
                                chessState[i + 2, j] == -1 && 
                                chessState[i + 3, j] == -1 &&
                                chessState[i + 4, j] == -1)
                            {
                                flag = -1;
                                return flag;
                            }

                            //左斜线
                            if (chessState[i, j] == -1 && 
                                chessState[i + 1, j - 1] == -1 &&
                                chessState[i + 2, j - 2] == -1 && 
                                chessState[i + 3, j - 3] == -1 &&
                                chessState[i + 4, j - 4] == -1)
                            {
                                flag = -1;
                                return flag;
                            }
                        }
                    }
                }
                for (int i = 11; i < 15; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        //只需要判断纵向    
                        if (chessState[i, j] == -1 && 
                            chessState[i, j + 1] == -1 &&
                            chessState[i, j + 2] == -1 && 
                            chessState[i, j + 3] == -1 &&
                            chessState[i, j + 4] == -1)
                        {
                          
                            flag = -1;
                            return flag;
                        }
                    }
                }
                break;
            
        }
        
       

        return flag;
    }

}

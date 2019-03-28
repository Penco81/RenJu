using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_Rule : MonoBehaviour {
   
    //连子禁手
    public static bool BanHand1(int x, int y)
    {
        int[] tt = new int[9];//分别记录八个方向上的棋子个数
        int[] w = new int[4]; //"-""|""/""\"四条线上的棋子个数
        //j3表示在各个方向上形成3子都是黑子的情况的个数
        int j3 = 0;
        //j4表示在各个方向上形成4子都是黑子的情况的个数
        int j4 = 0;
        //j6表示各个方向上形成6子都是黑子的情况的个数
        int j6 = 0;

        //水平方向
        for (int i1 = 1; i1 < 5; i1++)
        {
            if(x-i1>=0&&x+1<15)
            {
                if (Done_Chess.chessState[x - i1, y] == 1)
                {
                    tt[1]++;
                }
                else if (Done_Chess.chessState[x + 1, y] == -1||
                         Done_Chess.chessState[x - i1, y] == -1)
                {
                    tt[1] = 0;
                    break;
                }
            }
            
        }

        //右
        for (int i2 = 1; i2 < 5; i2++)
        {      
            if(x+i2<15&&x-1>=0)
            {
                if (Done_Chess.chessState[x + i2, y] == 1)
                {
                    tt[5]++;
                }
                //else if (Chess.chessState[x - 1, y] == -1 || Chess.chessState[x - i2, y] == -1)
                else if (Done_Chess.chessState[x - 1, y] == -1||
                    Done_Chess.chessState[x + i2, y] == -1)
                {
                    tt[5] = 0;
                    break;
                }
            }
                 
        }

        w[0] = tt[1] + tt[5];

        //竖直方向
        //上
        for (int i3 = 1; i3 < 5; i3++)
        {
            if(y-i3>=0&&y+1<15)
            {
                if (Done_Chess.chessState[x, y - i3] == 1)
                {
                    tt[3]++;
                } 
                else if (Done_Chess.chessState[x, y + 1] == -1||
                    Done_Chess.chessState[x, y - i3] == -1)
                {
                    tt[3] = 0;
                    break;
                }
            }
            
        }

        //下
        for (int i4 = 1; i4 < 5; i4++)
        {
            if(y+i4<15&&y-1>=0)
            {
                if (Done_Chess.chessState[x, y + i4] == 1)
                {
                    tt[7]++;
                }
                else if (Done_Chess.chessState[x, y - 1] == -1||
                         Done_Chess.chessState[x, y + i4] == -1)
                {
                    tt[7] = 0;
                    break;
                }
            }
           
        }

        w[1] = tt[3] + tt[7];


        //左上
        for (int i5 = 1; i5 < 5; i5++)
        {
            if(x-i5>=0&&y-i5>=0&&x+1<15&&y+1<15)
            {
                if (Done_Chess.chessState[x - i5, y - i5] == 1)
                {
                    tt[2]++;
                }
                else if (Done_Chess.chessState[x + 1, y + 1] == -1|| 
                         Done_Chess.chessState[x - i5, y - i5] == -1)
                {
                    tt[2] = 0;
                    break;
                }
            }
       
        }

        //右下
        for (int i6 = 1; i6 < 5; i6++)
        {
            if(x+i6<15&&y+i6<15&&x-1>=0&&y-1>=0)
            {
                if (Done_Chess.chessState[x + i6, y + i6] == 1)
                {
                    tt[6]++;
                }
                else if (Done_Chess.chessState[x - 1, y - 1] == -1 || 
                         Done_Chess.chessState[x + i6, y + i6] == -1)
                {
                    tt[6] = 0;
                    break;
                }
            }  
        }

      
        w[2] = tt[2] + tt[6];
        

        //左下
        for (int i7 = 1; i7 < 5; i7++)
        {
            if(x-i7>=0&&y+i7<15&&x+1<15&&y-1>=0)
            {
                if (Done_Chess.chessState[x - i7, y + i7] == 1)
                {
                    tt[8]++;
                }
                else if (Done_Chess.chessState[x + 1, y - 1] == -1|| 
                         Done_Chess.chessState[x - i7, y + i7] == -1)
                {
                    tt[8] = 0;
                    break;
                }
            }
           
        }

        //右上
        for (int i8 = 1; i8 < 5; i8++)
        {
            if(x+i8<15&&y-i8>=0&&x-1>=0&&y+1<15)
            {
                if (Done_Chess.chessState[x + i8, y - i8] == 1)
                {
                    tt[4]++;
                }
                else if (Done_Chess.chessState[x - 1, y + 1] == -1||
                    Done_Chess.chessState[x + i8, y - i8] == -1)
                {
                    tt[4] = 0;
                    break;
                }
            }
           
        }


        w[3] = tt[4] + tt[8];


        for (int i = 0; i < 4; i++)
        {
            if (w[i] == 2)
            {
                j3++;
            }
            else if (w[i] == 3)
            {
                j4++;
            }
            else if (w[i] == 5)
            {
                j6++;
            }
        }
        
        if (j3 >= 2 && j4 != 2 || j4 >= 2 || 
            j3 >= 2 && j4 >= 1 || j6 >= 1||j3>=1&&j4>=1)
        {
            return true;
        }

        return false;


    }

    //非连子禁手
    public static bool BanHand2(int x,int y)
    {
        //三三禁手
        if(x-3>=0&&x+3<15&&
            y+4<15&&y-4>=0)
        {     
            if ((Done_Chess.chessState[x - 1, y] == 1 && Done_Chess.chessState[x - 2, y] == 1&& 
                Done_Chess.chessState[x - 3, y] == 0 && Done_Chess.chessState[x + 1, y] == 0||
                Done_Chess.chessState[x + 1, y] == 1 && Done_Chess.chessState[x + 2, y] == 1&& 
                Done_Chess.chessState[x + 3, y] == 0 && Done_Chess.chessState[x - 1, y] == 0)&& 
                (Done_Chess.chessState[x, y + 1] == 0 && Done_Chess.chessState[x, y + 2] == 1&& 
                Done_Chess.chessState[x, y + 3] == 1 && Done_Chess.chessState[x, y + 4] == 0&& 
                Done_Chess.chessState[x, y - 1] == 0) || Done_Chess.chessState[x, y - 1] == 0&& 
                Done_Chess.chessState[x, y - 2] == 1 && Done_Chess.chessState[x, y - 3] == 1&& 
                Done_Chess.chessState[x, y - 4] == 0 && Done_Chess.chessState[x, y + 1] == 0)
                {
                    return true;
                }
        }
        if(y+3<15&&y-3>=0&&
            x+4<15&&x-4>=0)
        {
            if ((Done_Chess.chessState[x, y + 1] == 1 && Done_Chess.chessState[x, y + 2] == 1 &&
              Done_Chess.chessState[x, y + 3] == 0 && Done_Chess.chessState[x, y - 1] == 0 ||
              Done_Chess.chessState[x, y - 1] == 1 && Done_Chess.chessState[x, y - 2] == 1 &&
              Done_Chess.chessState[x, y - 3] == 0 && Done_Chess.chessState[x, y + 1] == 0) &&
              (Done_Chess.chessState[x + 1, y] == 0 && Done_Chess.chessState[x + 2, y] == 1 &&
              Done_Chess.chessState[x + 3, y] == 1 && Done_Chess.chessState[x + 4, y] == 0 &&
              Done_Chess.chessState[x - 1, y] == 0 || Done_Chess.chessState[x - 1, y] == 0 &&
              Done_Chess.chessState[x - 2, y] == 1 && Done_Chess.chessState[x - 3, y] == 1 &&
              Done_Chess.chessState[x + 1, y] == 0 && Done_Chess.chessState[x - 4, y] == 0))
            {
                return true;
            }

        }
        if(x+4<15&&y-4>=0&&x-4>=0&&y+4<15)
        {
            if ((Done_Chess.chessState[x - 1, y - 1] == 1 && Done_Chess.chessState[x - 2, y - 2] == 1 &&
                Done_Chess.chessState[x - 3, y - 3] == 0 && Done_Chess.chessState[x + 1, y + 1] == 0 ||
                Done_Chess.chessState[x + 1, y + 1] == 1 && Done_Chess.chessState[x + 2, y + 2] == 1 &&
                Done_Chess.chessState[x + 3, y + 3] == 0 && Done_Chess.chessState[x - 1, y - 1] == 0) &&
                (Done_Chess.chessState[x + 1, y - 1] == 0 && Done_Chess.chessState[x + 2, y - 2] == 1 &&
                Done_Chess.chessState[x + 3, y - 3] == 1 && Done_Chess.chessState[x + 4, y - 4] == 0 ||
                Done_Chess.chessState[x - 1, y + 1] == 0 && Done_Chess.chessState[x - 2, y + 2] == 1 &&
                Done_Chess.chessState[x - 3, y + 3] == 1 && Done_Chess.chessState[x - 4, y + 4] == 0))
            {
                return true;
            }
        }  
        if(x+4<15&&y+4<1&&x-4>=0&y-4>=0)
        {
             if ((Done_Chess.chessState[x - 1, y + 1] == 1 && Done_Chess.chessState[x - 2, y + 2] == 1 &&
                Done_Chess.chessState[x - 3, y + 3] == 1 && Done_Chess.chessState[x + 1, y - 1] == 0 ||
                Done_Chess.chessState[x + 1, y - 1] == 1 && Done_Chess.chessState[x + 2, y - 2] == 1 &&
                Done_Chess.chessState[x + 3, y - 3] == 0 && Done_Chess.chessState[x - 1, y + 1] == 0) &&
                (Done_Chess.chessState[x + 1, y + 1] == 0 && Done_Chess.chessState[x + 2, y + 2] == 0 &&
                Done_Chess.chessState[x + 3, y + 3] == 1 && Done_Chess.chessState[x + 4, y + 4] == 0 ||
                Done_Chess.chessState[x - 1, y - 1] == 0 && Done_Chess.chessState[x - 2, y - 2] == 0 &&
                Done_Chess.chessState[x - 3, y - 3] == 1 && Done_Chess.chessState[x - 4, y - 4] == 0))
            {
                return true;
            }
        }       
        if(x-6>=0&&x+5<15&&y-6>=0&&y+6<15)
        {
            //四四有界禁手
            if ((Done_Chess.chessState[x - 1, y] == 1 && Done_Chess.chessState[x - 2, y] == 0 &&
                Done_Chess.chessState[x - 3, y] == 1 && Done_Chess.chessState[x - 4, y] == 1 &&
                Done_Chess.chessState[x - 6, y] == 0 && Done_Chess.chessState[x - 5, y] == 0 &&
                Done_Chess.chessState[x + 2, y] == 1 && Done_Chess.chessState[x + 3, y] == 1 &&
                Done_Chess.chessState[x + 1, y] == 0 && Done_Chess.chessState[x + 4, y] == 0 &&
                Done_Chess.chessState[x + 5, y] == 0) || (Done_Chess.chessState[x, y + 1] == 0 &&
                Done_Chess.chessState[x, y + 2] == 1 && Done_Chess.chessState[x, y + 3] == 1 &&
                Done_Chess.chessState[x, y + 4] == -1 && Done_Chess.chessState[x, y + 5] == 0 &&
                Done_Chess.chessState[x, y - 1] == 1 && Done_Chess.chessState[x, y - 2] == 0 &&
                Done_Chess.chessState[x, y - 3] == 1 && Done_Chess.chessState[x, y - 4] == 1 &&
                Done_Chess.chessState[x, y - 5] == -1 && Done_Chess.chessState[x, y - 6] == 0) ||
                (Done_Chess.chessState[x, y + 1] == 1 && Done_Chess.chessState[x, y + 2] == 0 &&
                Done_Chess.chessState[x, y + 3] == 1 && Done_Chess.chessState[x, y + 4] == 1 &&
                Done_Chess.chessState[x, y + 5] == -1 && Done_Chess.chessState[x, y + 6] == 0 &&
                Done_Chess.chessState[x, y - 1] == 0 && Done_Chess.chessState[x, y - 2] == 1 &&
                Done_Chess.chessState[x, y - 3] == 1 && Done_Chess.chessState[x, y - 4] == -1 &&
                Done_Chess.chessState[x, y - 5] == -1) || (Done_Chess.chessState[x, y + 1] == 0 &&
                Done_Chess.chessState[x, y + 2] == 1 && Done_Chess.chessState[x, y + 3] == 1 &&
                Done_Chess.chessState[x, y + 4] == -1 && Done_Chess.chessState[x, y + 5] == 0 &&
                Done_Chess.chessState[x, y - 6] == 0 && Done_Chess.chessState[x, y - 1] == 1 &&
                Done_Chess.chessState[x, y - 2] == 0 && Done_Chess.chessState[x, y - 3] == 1 &&
                Done_Chess.chessState[x, y - 4] == 1 && Done_Chess.chessState[x, y - 5] == -1))
            {
                return true;
            }
        }             
        //判断禁手之前，判断数组不会溢出
        if(x-3>=0&&y+3<15&&x+3<15&&y-3>=0)
        {
            //四四无界禁手
            if ((Done_Chess.chessState[x - 1, y] == 1 && Done_Chess.chessState[x - 2, y] == 0 &&
                Done_Chess.chessState[x - 3, y] == 1 && Done_Chess.chessState[x + 1, y] == 1 &&
                Done_Chess.chessState[x + 2, y] == 0 && Done_Chess.chessState[x + 3, y] == 1) ||
                (Done_Chess.chessState[x, y + 1] == 1 && Done_Chess.chessState[x, y + 2] == 0 &&
                Done_Chess.chessState[x, y + 3] == 1 && Done_Chess.chessState[x, y - 1] == 1 &&
                Done_Chess.chessState[x, y - 2] == 0 && Done_Chess.chessState[x, y - 3] == 1) ||
                (Done_Chess.chessState[x + 1, y + 1] == 1 && Done_Chess.chessState[x + 2, y + 2] == 0 &&
                Done_Chess.chessState[x + 3, y + 3] == 1 && Done_Chess.chessState[x - 1, y - 1] == 1 &&
                Done_Chess.chessState[x - 2, y - 2] == 0 && Done_Chess.chessState[x - 3, y - 3] == 1) ||
                (Done_Chess.chessState[x - 1, y + 1] == 1 && Done_Chess.chessState[x - 2, y + 2] == 0 &&
                Done_Chess.chessState[x - 3, y + 3] == 1 && Done_Chess.chessState[x + 1, y - 1] == 1 &&
                Done_Chess.chessState[x + 2, y - 2] == 0 && Done_Chess.chessState[x + 3, y - 3] == 1))
            {
                return true;
            }
        }
        return false;          
    }
}

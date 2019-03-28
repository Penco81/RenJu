using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point
{
    public int row;
    public int col;
    public Vector2 pos;
};

public class GameManager : MonoBehaviour {

    public float startR;
    public float startC;
    public float gap;
    public GameObject blackWin;
    public GameObject whiteWin;
    private bool isOver = false;
    private const int amount = 15;

    public GameObject black;
    public GameObject white;
    //0 black 1black 2 white
    private int[,] map = new int[15,15];
    private List<GameObject> save = new List<GameObject>();
    private int turn = 1;

	// Use this for initialization
	void Start () {
        ResetGame();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
            return;
        }
		if(Input.GetMouseButton(0) && !isOver)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.x >= -7.8f && mousePos.x <= 7.8f && mousePos.y <= 7.8f && mousePos.y >= -7.8f)
            {
                Point pos = GetPos(mousePos);
                if (map[pos.row, pos.col] == 0)
                {
                    map[pos.row, pos.col] = turn;
                    GameObject go;
                    if (turn == 1)
                    {
                        go = Instantiate(black, pos.pos, Quaternion.identity);
                    }
                    else
                    {
                        go = Instantiate(white, pos.pos, Quaternion.identity);
                    }
                    save.Add(go);
                    if(Check(turn, pos.row, pos.col))
                    {
                        Win(turn);
                        return;
                    }
                    turn = turn == 1 ? 2 : 1;
                }
            }
        }
	}

    Point GetPos(Vector3 v)
    {
        int x = (int)((v.x - startC + gap/ 2)/ gap);
        int y = (int)((startR - v.y + gap/2)/ gap);
        float realX = startC + x * gap;
        float reaY = startR - y * gap;
        Point p;
        p.row = y;
        p.col = x;
        p.pos = new Vector3(startC + x * gap, startR - y * gap, 0);
        return p;
    }

    void ResetGame()
    {
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                map[i, j] = 0;
            }
        }
        for(int i = 0; i < save.Count; i++)
        {
            Destroy(save[i]);
        }
        turn = 1;
        isOver = false;
        blackWin.SetActive(false);
        whiteWin.SetActive(false);
    }

    bool Check(int val, int row, int col)
    {
        // -
        int ans = 0;
        for(int i = -1; i <= 1; i+= 2)
        {
            for(int j = 1; j <= 5; j++)
            {
                int newC = col + i * j;
                if (newC < 0 || newC >= 15)
                    continue;
                if(map[row, newC] != val)
                {
                    break;
                }
                ++ans;
            }
        }
        if (ans >= 4)
            return true;
        // |
        ans = 0;
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = 1; j <= 5; j++)
            {
                int newR = row + i * j;
                if (newR < 0 || newR >= 15)
                    continue;
                if (map[newR, col] != val)
                {
                    break;
                }
                ++ans;
            }
        }
        if (ans >= 4)
            return true;
        // /
        ans = 0;
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = 1; j <= 5; j++)
            {
                int newR = row + i * j;
                int newC = col + i * j;
                if (newC < 0 || newC >= 15 || newR < 0 || newR >= 15)
                    continue;
                if (map[newR, newC] != val)
                {
                    break;
                }
                ++ans;
            }
        }
        if (ans >= 4)
            return true;
        // \
        ans = 0;
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = 1; j <= 5; j++)
            {
                int newR = row + i * j;
                int newC = col - i * j;
                if (newC < 0 || newC >= 15 || newR < 0 || newR >= 15)
                    continue;
                if (map[newR, newC] != val)
                {
                    break;
                }
                ++ans;
            }
        }
        if (ans >= 4)
            return true;
        return false;
    }

    void Win(int val)
    {
        if(val == 1)
        {
            blackWin.SetActive(true);
        }
        else
        {
            whiteWin.SetActive(true);
        }
        isOver = true;
    }
}

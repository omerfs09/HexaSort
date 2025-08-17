using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    
    public const float STACK_SPACE = 0.15f;
    public const  float HEXAGON_R = 0.42f;
    public const  int MOVESKILL_PRICE = 100;
    public const  int CLEARSKILL_PRICE = 75;
    public const  int REFRESHDESK_PRICE = 25;
    public void Awake()
    {
        
    }
    public static Vector3 HexPosition(int col,int row,float r = HEXAGON_R)
    {
        
        if(col % 2 == 0)
        {
            return Vector3.right*col* r * 1.5f + Vector3.forward* -1.73f*row*r;
        }
        else
        {
            return Vector3.right * col * r * 1.5f + Vector3.forward * -1.73f * row*r - Vector3.forward * r * 0.866f;
        }
    } 
    public static Color ColorEnumToColor(Colors color)
    {
        switch (color)
        {
            case Colors.Red:
                return Color.red;
                break;
            case Colors.Blue:
                return Color.cyan;
            case Colors.Green:
                return Color.green;
            case Colors.Black:
                return Color.black;
                break;
            case Colors.Cyan:
                return Color.cyan;
            case Colors.Pink:
                return new Color(255,100,100);
            case Colors.Purple:
                return Color.magenta;
                break;
            case Colors.White:
                return Color.white;
            case Colors.Yellow:
                return Color.yellow;
        }
        return Color.white;
    }
}

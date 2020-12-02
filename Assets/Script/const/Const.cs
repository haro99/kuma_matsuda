using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : MonoBehaviour
{

    public enum Direction
    {
        none = 0,
        up,
        left,
        down,
        right
    }

    public enum GameStatus
    {
        wait = 0,
        waitDiceClick = 1,
        diceWait = 2,
        playerMenuSelectWait = 3,
        playerMove = 4,
        enemyMove = 5,
        userWaySelect = 6,
        clear = 7,
        gameOver = 8,
        special = 9 ,
        special_effect = 10 ,
        tutorial = 11, 
    }


    public enum Tile
    {
        // item
        item1 = 1,
        item2 = 2,
        item3 = 3,
        item4 = 4,
        randomItem = 5,

        plain = 9,

        // start and goal
        start = -2,
        goal = -1,

        // other
        house = 10,
    }

    public enum TileColor
    {
        //none   = 0,
        orange = 1,
        yellow = 2,
        green = 3,
        blue = 4,
        ao = 5,
        aka = 6,
        gray = 9
    }

    public enum Item
    {
        meat = 1,
        egg = 2,
        vegetables = 3,
        fish = 4,
    }

    public enum Animation
    {
        StandTrigger,
        StandBackTrigger,
        WalkTrigger,
        WalkBackTrigger,
        RunTrigger,
        RunBackTrigger,
        LaughTrigger,
        SadTrigger,
        AngerTrigger
    }


    public enum Special
    {
        Dice_Plus_One = 0,
        Item_x2 = 1,
        Dice_Free_15Sec = 2  
    }

}
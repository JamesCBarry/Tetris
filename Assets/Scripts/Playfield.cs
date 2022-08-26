using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static int w = 12; // Width of the playfield
    public static int h = 24; // Height of the playfield

    public static Transform[,] grid = new Transform[w, h]; //Build the full grid

    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y)); //Round out any rotations to the nearest number

    }

    //Is the piece great than a width of 0, less than 10, and higher then a height of 0? This filters through every single individual game object.
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }

    //Loops through an entire row deleting any gameobjects and then setting the grid area to null.
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    //Loops through every item on the grid. If it's not null (i.e. currently there), it destroys it.
    public static void deletePlayfield()
    {
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
                if (grid[x, y] != null)
                    Destroy(grid[x, y].gameObject);
        }
    }

    //Loops through one specific row. If a grid item is not null, it will move that item down one, set the old grid space to null, then move the position down one as well??
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //Performs the decrease row function on every row above it
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    //Loops through every grid position on a specific row to check if it is full.
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    //Loops throough the full height of the playing board. Checks to see if a row is full. If it is, it deletes the row, then pulls down every single row above it. The "--y" allows the function to then restart at that base row.
    public static void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))  //Checks to see if the row is full.
            {
                deleteRow(y); //If row is full, it deletes it.
                decreaseRowsAbove(y + 1); //It then decreases every single row above it. (Function loops through full height of the grid).
                --y; //After that, the function restarts at the same row level.
                Controller.instance.UpdateScore();
            }
        }
    }
}
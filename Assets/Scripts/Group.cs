using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    float lastFall = 0;
    float lastDown = 0.05f;

    void Start()
    {
        if (!isValidGridPosition()) //This is just NOT triggering for some reason.
        {
            Controller.instance.GameOver();
            Destroy(gameObject);
        }
    }

    bool isValidGridPosition()
    {
        foreach (Transform child in transform) //Loops through every child gameobject transform
        {
            Vector2 v = Playfield.roundVec2(child.position); //Stores the current position in a variable.
             if (!Playfield.insideBorder(v)) //If the child position is outside grid, returns false. Function returns false.
                return false;

            if (Playfield.grid[(int)v.x, (int)v.y] != null &&
                Playfield.grid[(int)v.x, (int)v.y].parent != transform) //If the current block is not empty, AND the blocks parent is not the current parent, return false.
                return false;
        }
        return true;
    }
    void updateGrid()
    {
        //This function sets all the old parts of the grid to null that used to be there.

        for (int y = 0; y < Playfield.h; ++y) // Loops through the height of the playfield.
            for (int x = 0; x < Playfield.w; ++x) // Loops through the width of the playfield.
                if (Playfield.grid[x, y] != null) // If a position has something in it then....
                    if (Playfield.grid[x, y].parent == transform) // If the position IS part of this current block group
                        Playfield.grid[x, y] = null; //Set the playfield grid at that location to null.

        foreach (Transform child in transform) //Loops through all the children
        {
            Vector2 v = Playfield.roundVec2(child.position); //Stores the current position. Why does "roundVec2" need to be in Playfield?
            Playfield.grid[(int)v.x, (int)v.y] = child; //Adds the current position as a child of the grid (i.e. IS THERE SOMETHING THERE AKA THE NULL PART OF THE GRID!!!).
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (isValidGridPosition())
            {
                updateGrid();
            }
            else
                transform.position += new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (isValidGridPosition())
            {
                updateGrid();
            }
            else
                transform.position += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.eulerAngles += new Vector3(0, 0, -90);
            if (isValidGridPosition())
            {
                updateGrid();
                FindObjectOfType<Audio>().FlipClip();
            }
            else
                transform.eulerAngles += new Vector3(0, 0, +90);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Time.time - lastFall > lastDown || Time.time - lastFall >= 1)
        {
            transform.position += new Vector3(0, -1, 0);
            if (isValidGridPosition())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                Playfield.deleteFullRows();

                FindObjectOfType<Spawner>().NextBlock();
                FindObjectOfType<Audio>().PlacedClip();

                enabled = false;
            }
            lastFall = Time.time;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite[] backgroundGroup;

    public void UpdateBackground()
    {
        int x = Random.Range(0, backgroundGroup.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = backgroundGroup[x];
    }
}

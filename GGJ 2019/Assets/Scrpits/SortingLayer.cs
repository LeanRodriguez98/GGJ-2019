using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    public List<SpriteRenderer> spritesToSort;

	void Update ()
    {
        for (int i = 0; i < spritesToSort.Count; i++)
        {
            spritesToSort[i].sortingOrder = (int)(spritesToSort[i].gameObject.transform.position.y * -100);
        }
	}
}

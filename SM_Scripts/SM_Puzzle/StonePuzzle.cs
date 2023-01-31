using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePuzzle : MonoBehaviour
{
    [SerializeField] private Puzzle puzzle;

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player" || coll.gameObject.tag == "Stone")
        {
            if (puzzle.count == 1)
            {
                Destroy(gameObject);
                puzzle.count--;
            }

        }
    }
}

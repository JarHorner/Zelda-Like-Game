using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    #region Variables
    //list of items that can be drops
    [SerializeField] private List<GameObject> drops = new List<GameObject>();
    //populate table from most frequent pecentage to least frequent
    [SerializeField] private int[] table = 
    { 
        33, //Item 1
        28, //Item 2
        27, //Item 3
        10, //Item 4
        2 //Item 5
    };
    private int total;
    private int randomNumber;
    #endregion

    #region Methods

    void Start()
    {
        //gets the total weight
        foreach (int item in table)
        {
            total += item;
        }
    }

    public void DropItem()
    {
        //find a random number within the total weight
        randomNumber = Random.Range(0, total);
        Debug.Log(randomNumber);

        //loops through weight table and compares the random number to each weight.
        //if the number is less or equal, an item correlating to that weight is spawned
        //if the number is greater, the weight is subtracted from the random number and 
        //the next weight is checked until item to drop is found.
        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                //Drop item
                Debug.Log($"Dropped Item: {drops[i]}" );
                if (drops[i] != null)
                {
                    Instantiate(drops[i], this.transform.position, Quaternion.identity);
                }
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }
    }

    #endregion
}

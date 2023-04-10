using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // The number of rows and columns of the game board.
    private int numRows = 4;
    private int numCols = 6;

    // A 2D array representing the numbers in each cell of the game board.
    private int[,] allNumberCell;
    public List<int> allNumber = new List<int>();
    // The prefab for the game board cell.
    public GameObject perfab;
    public Transform parent;
    
    public int chosenNumber;
    public GameObject curSelect;

    // Start is called before the first frame update.
    void Start()
    {
        // Populate the list of numbers.
        for (var i = 0; i < numRows * numCols / 2; i++) {
            allNumber.Add(i);
            allNumber.Add(i);
        }

        // Generate a random order for the numbers in the list.
        var range = GenerateRandomList(numRows * numCols, 0, allNumber.Count);

        // Initialize the 2D array with the random numbers.
        allNumberCell = new int[numCols, numRows];
        var curIndex = 0;
        for (int y = 0; y < allNumberCell.GetLength(1); y++)
        {
            for (int x = 0; x < allNumberCell.GetLength(0); x++)
            {
                allNumberCell[x, y] = allNumber[range[curIndex]];
                curIndex++;

                // Instantiate a new game board cell and set its text.
                var go = GameObject.Instantiate(perfab, parent);
                int xInt = x;
                int yInt = y;
                go.GetComponentInChildren<Text>().text = allNumberCell[x, y].ToString();
                go.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ChooseBtn(xInt, yInt, go);
                });
            }
        }
    }

    public void ChooseBtn(int x, int y, GameObject obj) {
        // Highlight the selected game board cell.
        obj.GetComponent<Image>().color = Color.yellow;

        if (curSelect != null)
        {
            // If the player has already selected a game board cell.
            if (curSelect != obj)
            {
                var number = allNumberCell[x, y];

                // If the selected game board cell contains the same number as the previous selection.
                if (chosenNumber == number)
                {
                    // Disable both game board cells and color them green.
                    curSelect.GetComponent<Button>().interactable = false;
                    obj.GetComponent<Button>().interactable = false;
                    curSelect.GetComponent<Image>().color = Color.green;
                    obj.GetComponent<Image>().color = Color.green;
                    curSelect = null;
                }
                else
                {
                    // If the selected game board cell contains a different number than the previous selection, unhighlight both.
                    chosenNumber = -1;
                    curSelect.GetComponent<Image>().color = Color.white;
                    obj.GetComponent<Image>().color = Color.white;
                    curSelect = null;
                }
            }
        }
        else
        {
            // If the player has not yet selected a game board cell, select this one.
            chosenNumber = allNumberCell[x, y];
            curSelect = obj;
        }
    }



    /// <summary>
    /// </summary>
    /// <param name="length"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    // Generates a random list of integers within a range
    public static List<int> GenerateRandomList(int length, int min, int max)
{
    //store the random numbers
    List<int> randomList = new List<int>();
    
    // Only generate random numbers if the length requested is within the range
    if (length <= (max - min))
    {
        for (var i = 0; i < length; i++)
        {
            int random = Random.Range(min, max);
            
            // If the number is already in the list, decrement the index and continue
            if (randomList.Contains(random))
            {
                i--;
                continue;
            }
            // Otherwise, add the number to the list
            else
            {
                randomList.Add(random);
            }
        }
    }
    return randomList;
}

//Restarts the game
public void ReStartGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

// This function is called once per frame
void Update()
{

}

}

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelParser : MonoBehaviour
{
    public string filename;
    
    [FormerlySerializedAs("Rock")] 
    public GameObject RockPrefab;
    
    [FormerlySerializedAs("Brick")] 
    public GameObject BrickPrefab;
    
    [FormerlySerializedAs("QuestionBox")] 
    public GameObject QuestionBoxPrefab;
    
    [FormerlySerializedAs("Stone")] 
    public GameObject StonePrefab;

    [FormerlySerializedAs("Water")] 
    public GameObject WaterPrefab;
    
    [FormerlySerializedAs("Goal")] 
    public GameObject GoalPrefab;
    
    public Transform levelRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");
        
        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        // Go through the rows from bottom to top
        int row = 0;
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            int column = 0;
            char[] letters = currentLine.ToCharArray();
            foreach (var letter in letters)
            {
                // Instantiate a new GameObject that matches the type specified by letter
                var rockObject = Instantiate(RockPrefab);
                var brickObject = Instantiate(BrickPrefab);
                var testObject = Instantiate(StonePrefab);
                var questionObject = Instantiate(QuestionBoxPrefab);
                var waterObject = Instantiate(WaterPrefab);
                var goalObject = Instantiate(GoalPrefab);
                
                if (letter == 'x')
                {
                    rockObject.transform.position = new Vector3(column, row, 0f);
                }
                
                if (letter == 'b')
                {
                    brickObject.transform.position = new Vector3(column, row, 0f);
                }
                
                if (letter == '?')
                {
                    questionObject.transform.position = new Vector3(column, row, 0f);
                }

                if (letter == 's')
                {
                    testObject.transform.position = new Vector3(column, row, 0f);
                }

                if (letter == 'w')
                {
                    waterObject.transform.position = new Vector3(column, row, 0f);
                }

                if (letter == 'g')
                {
                    goalObject.transform.position = new Vector3(column, row, 0f);
                }
                
                // Position the new GameObject at the appropriate location by using row and column
                // Parent the new GameObject under levelRoot
                column++;
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in levelRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}

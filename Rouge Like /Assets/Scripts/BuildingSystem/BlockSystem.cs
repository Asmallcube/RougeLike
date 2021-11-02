using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSystem : MonoBehaviour
{
    public int blockHealth;



    // Arrays for soild blocks.
    // The two arrays need to match.

    [SerializeField]
    private Sprite[] soildBlocks;
    [SerializeField]
    private string[] soildNames;


    // Arrays for background blocks.
    // The two arrays need to match.

    [SerializeField]
    private Sprite[] backgroundBlocks;
    [SerializeField]
    private string[] backgroundNames;

    // Array to store all blocks created in Awake().
    public Block[] allBlocks;


    private void Awake()
    {
        // Inishalise allBlocks array.
        allBlocks = new Block[soildBlocks.Length + backgroundBlocks.Length];

        // Temp int to store block ID threw.
        int newBlockID = 0;

        // for loops to populate allBlocks Array.
        for (int i = 0; i < soildBlocks.Length; i++)
        {
            allBlocks[newBlockID] = new Block(newBlockID, soildNames[i], soildBlocks[i], true);
            Debug.Log("Soild block: allBlocks[" + newBlockID + "] = " + soildNames[i]);
            newBlockID++;
        }

        for (int i = 0; i < backgroundBlocks.Length; i++)
        {
            allBlocks[newBlockID] = new Block(newBlockID, backgroundNames[i], backgroundBlocks[i], false);
            Debug.Log("BackGround block: allBlocks[" + newBlockID + "] = " + backgroundNames[i]);
            newBlockID++;
        }
    }
}

public class Block
{
    public int blockID;
    public string blockName;
    public Sprite blockSprite;
    public bool isBlockSoild;
    public int blockHealth;

    public Block(int id, string name, Sprite sprite, bool isSoild)
    {
        blockID = id;
        blockName = name;
        blockSprite = sprite;
        isBlockSoild = isSoild;

        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{

    // Refrence to block system script.
    private BlockSystem blockSystem;

    // Variable to hold data regarding current block type.
    private int currentBlockID;
    private Block currentBlock;

    // Varibles for the block template (the thingy that follows the mouse).
    private GameObject blockTemplate;
    private SpriteRenderer currentRend;

    // Bools to control building system.
    private bool buildModeOn = false;
    private bool buildBlocked = false;
    private bool buildBlockedDistance = false;

    // Float to store size of blocks when placing in world.
    [SerializeField]
    private float blockSizeModifaction;

    // Layermask to control raycasting.
    [SerializeField]
    private LayerMask soildNoBuildLayer;
    [SerializeField]
    private LayerMask backingNoBuildLayer;
    [SerializeField]
    private LayerMask allBlocksLayer; 

    // Referance to the player object.
    private GameObject playerObject;

    // Distance from the player that blocks can be placed.
    [SerializeField]
    private float maxBuildDistance;

    private void Awake()
    {
        // Store refrence to block system script.
        blockSystem = GetComponent<BlockSystem>();

        // Find and store player object reference.
        playerObject = GameObject.Find("Player");
    }

    private void Update()
    {
        // TEMP enter build mode when E key is pressed.
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Flip bool.
            buildModeOn = !buildModeOn;

            // If there is a current template delete it.
            if (blockTemplate != null)
            {
                Destroy(blockTemplate);
            }

            // If no block type is selected
            if (currentBlock == null)
            {
                // Ensure allBlocks array is ready.
                if (blockSystem.allBlocks[currentBlockID] != null)
                {
                    // Get a new current block using the ID varible.
                    currentBlock = blockSystem.allBlocks[currentBlockID];
                }
            }

            if (buildModeOn)
            {
                // Create a new object for block template.
                blockTemplate = new GameObject("currentBlockTemplate");
                // Add and store reference to a Sprite rendere on the template object.
                currentRend = blockTemplate.AddComponent<SpriteRenderer>();
                // Set the sprite of the template object to match the current block type.
                currentRend.sprite = currentBlock.blockSprite;
            }
        }

        if (buildModeOn && blockTemplate != null)
        {
            // Get mouse pos and round it to a grid and place template on that grid.
            float newPosX = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / blockSizeModifaction) * blockSizeModifaction;
            float newPosY = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / blockSizeModifaction) * blockSizeModifaction;
            blockTemplate.transform.position = new Vector2(newPosX, newPosY);
            currentRend.sortingOrder = 10;

            // Generate a ray to figure out wether a block can be built. 
            RaycastHit2D rayHit;

            if (currentBlock.isBlockSoild == true)
            {
                rayHit = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, soildNoBuildLayer);
            } else
            {
                rayHit = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, backingNoBuildLayer);
            }

            if (rayHit.collider != null)
            {
                buildBlocked = true;
            } else
            {
                buildBlocked = false;
            }

            if (Vector2.Distance(playerObject.transform.position, blockTemplate.transform.position) > maxBuildDistance)
            {
                buildBlockedDistance = true;
                buildBlocked = true;
            } else
            {
                buildBlockedDistance = false;
            }

            if (buildBlocked)
            {
                // If cant build change template color to red.
                currentRend.color = new Color(1f, 0f, 0f, 0.5f);
            } else
            {
                // Reset color if can build. 
                currentRend.color = new Color(1f, 1f, 1f, 1f);
            }

            // When LEFT mouse button is clicked place block at block template location if nothing is stopping it.
            if (Input.GetMouseButtonDown(0) && buildBlocked == false)
            {
                GameObject newBlock = new GameObject(currentBlock.blockName);
                newBlock.transform.position = blockTemplate.transform.position;
                SpriteRenderer newRend = newBlock.AddComponent<SpriteRenderer>();
                newRend.sprite = currentBlock.blockSprite;

                // Adding soild block spesific stuff.
                if (currentBlock.isBlockSoild == true)
                {
                    newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 9;
                    newRend.sortingOrder = 20;
                } else
                // Adding backing block spesific stuff.
                {
                    newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 12;
                    newRend.sortingOrder = 9;
                }
            }

            // When RIGHT mouse button is clicked destory block at block templates location if you can build there.
            if (Input.GetMouseButtonDown(1) && blockTemplate != null && buildBlockedDistance == false)
            {
                RaycastHit2D destoryHit = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, allBlocksLayer);

                if (destoryHit.collider != null)
                {
                    Destroy(destoryHit.collider.gameObject);
                }

            }
        }
    }
}

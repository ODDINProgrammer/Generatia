using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPieceGeneration : MonoBehaviour
{
    [Header("World components")]
    [SerializeField] private List<GameObject> worldPieces;
    [SerializeField] private List<int> chanceToSpawn;
    private bool hasGenerated = false;

    [Header("Timer")]
    [SerializeField] private float generateAfter;
    private float timer;

    private int pieceOffset;

    private RaycastHit hit;

    private void Start()
    {
        pieceOffset = (int)transform.localScale.x;
    }

    private void Update()
    {
        if (timer <= generateAfter && !hasGenerated)
            timer += Time.deltaTime;

        if (timer >= generateAfter)
            GenerateWorldPiece();
    }

    public void GenerateWorldPiece()
    {
        hasGenerated = true;

        //  GENERATE UP;
        #region
        if (Physics.Raycast(new Ray(transform.position, Vector3.forward), out hit))
        { }
        else
        {
            //  GENERATE NEW PIECE
            GameObject newPiece = Instantiate(worldPieces[GetWorldPieceIndex()]);
            //  CALCULATE ITS POSITION
            Vector3 piecePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + pieceOffset);
            //  MOVE TO CALCULATED POSITION
            newPiece.transform.position = piecePosition;
        }
        #endregion

        //  GENERATE DOWN;
        #region
        if (Physics.Raycast(new Ray(transform.position, Vector3.back), out hit))
        { }
        else
        {
            //  GENERATE NEW PIECE
            GameObject newPiece = Instantiate(worldPieces[GetWorldPieceIndex()]);
            //  CALCULATE ITS POSITION
            Vector3 piecePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - pieceOffset);
            //  MOVE TO CALCULATED POSITION
            newPiece.transform.position = piecePosition;
        }
        #endregion

        //  GENERATE RIGHT;
        #region
        if (Physics.Raycast(new Ray(transform.position, Vector3.right), out hit))
        { }
        else
        {
            //  GENERATE NEW PIECE
            GameObject newPiece = Instantiate(worldPieces[GetWorldPieceIndex()]);
            //  CALCULATE ITS POSITION
            Vector3 piecePosition = new Vector3(transform.position.x + pieceOffset, transform.position.y, transform.position.z);
            //  MOVE TO CALCULATED POSITION
            newPiece.transform.position = piecePosition;
        }
        #endregion

        //  GENERATE LEFT;
        #region
        if (Physics.Raycast(new Ray(transform.position, Vector3.left), out hit))
        { }
        else
        {
            //  GENERATE NEW PIECE
            GameObject newPiece = Instantiate(worldPieces[GetWorldPieceIndex()]);
            //  CALCULATE ITS POSITION
            Vector3 piecePosition = new Vector3(transform.position.x - pieceOffset, transform.position.y, transform.position.z);
            //  MOVE TO CALCULATED POSITION
            newPiece.transform.position = piecePosition;
        }
        #endregion
    }

    private int GetWorldPieceIndex()
    {
        int pieceIndex = 0;
        int rand = Random.Range(0, 100+1);

        for(int i = 0; i < chanceToSpawn.Count; i++)
        {
            if (rand > chanceToSpawn[i])
                break;
            
            else
            {
                pieceIndex = i;
            }
        }

        return pieceIndex;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 3f, Color.yellow);
        Debug.DrawRay(transform.position, Vector3.left * 3f, Color.yellow);
        Debug.DrawRay(transform.position, Vector3.right * 3f, Color.yellow);
        Debug.DrawRay(transform.position, Vector3.back * 3f, Color.yellow);
    }
}

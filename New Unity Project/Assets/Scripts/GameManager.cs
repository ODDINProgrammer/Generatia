using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Accessors")]
    [SerializeField] private WorldPieceGeneration worldPieceGenerator;

    [Header("Timer")]
    [SerializeField] private float generateEachSecond;
    private float timer;

    private void Start()
    {
        timer = generateEachSecond;
    }

    private void Update()
    {
        if (timer <= generateEachSecond)
            timer += Time.deltaTime;

        if (timer >= generateEachSecond)
            worldPieceGenerator.GenerateWorldPiece();
    }
}

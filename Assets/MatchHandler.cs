using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : MonoBehaviour
{
    public static MatchHandler Instance;

    [Header("Prefab References")]
    [SerializeField] private Character playerPrefab;
    [SerializeField] private Character playerTwoPrefab;
    [SerializeField] private Character combatantPrefab;
    [SerializeField] private SlammableTarget slammableTargetPrefab;

    [Header("Spawn Properties")]
    [SerializeField] private Transform spawnCircleAreaCentre;
    [SerializeField] private float spawnCircleAreaRadius;
    [SerializeField] private float spawnMinimumDistanceDifference = 1.5f;
    [SerializeField] private float spawnMaximumDistanceDifference = 5f;
    [SerializeField] private float maximumDistancingAttempts = 10f;

    [Header("Camera Focus Properties")]
    [SerializeField] private CameraFocusPosition cameraFocuser;

    private List<Character> activeCharacters = new List<Character>();

    private int remainingSlammableTargetCount;

    [SerializeField] private GameObject matchEndScreenGameObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private void Start()
    {
        InitializePvE();
    }

    public void InitializePvE()
    {
        Vector2 randomPointInCircleOfSpawnAreaRadius = Random.insideUnitCircle * spawnCircleAreaRadius;

        Vector3 spawnPoint = spawnCircleAreaCentre.position + new Vector3(randomPointInCircleOfSpawnAreaRadius.x, transform.position.y, randomPointInCircleOfSpawnAreaRadius.y);

        Character playerOne = Instantiate(playerPrefab, spawnPoint, playerPrefab.transform.rotation);

        activeCharacters.Add(playerOne);

        Vector2 secondRandomPointInCircleOfSpawnAreaRadius;

        int distancingAttempts = 0;

        float spawnMinimumDistanceDifferenceSquared = spawnMinimumDistanceDifference * spawnMinimumDistanceDifference;

        float spawnMaximumDistanceDifferenceSquared = spawnMaximumDistanceDifference * spawnMaximumDistanceDifference;

        float distanceApartSquared;

        do
        {
            secondRandomPointInCircleOfSpawnAreaRadius = Random.insideUnitCircle * spawnCircleAreaRadius;

            distanceApartSquared = (randomPointInCircleOfSpawnAreaRadius - secondRandomPointInCircleOfSpawnAreaRadius).sqrMagnitude;

            distancingAttempts++;
        }
        while ((distanceApartSquared < spawnMinimumDistanceDifferenceSquared || distanceApartSquared > spawnMaximumDistanceDifferenceSquared) && distancingAttempts <= maximumDistancingAttempts);

        Vector3 secondSpawnPoint = spawnCircleAreaCentre.position + new Vector3(secondRandomPointInCircleOfSpawnAreaRadius.x, transform.position.y, secondRandomPointInCircleOfSpawnAreaRadius.y);

        Character playerTwo = Instantiate(playerTwoPrefab, secondSpawnPoint, playerTwoPrefab.transform.rotation);

        activeCharacters.Add(playerTwo);

        cameraFocuser.SetFocusTargetTranforms(playerOne.transform, playerTwo.transform);
    }

    public void InitializePvAi()
    {

    }

    public void InitializePvP()
    {

    }

    public void EvaluateMatchEnd(GameObject destroyedTarget)
    {
        if(destroyedTarget.GetComponent<Character>() != null)
        {
            matchEndScreenGameObject.SetActive(true);
        }
        else if(destroyedTarget.GetComponent<SlammableTarget>() != null)
        {
            remainingSlammableTargetCount--;

            if(remainingSlammableTargetCount == 0)
            {
                matchEndScreenGameObject.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : MonoBehaviour
{
    public static MatchHandler Instance;

    [Header("Prefab References")]
    [SerializeField] private Character playerPrefab;
    [SerializeField] private Character playerTwoPrefab;
    [SerializeField] private Character combatantOnePrefab;
    [SerializeField] private Character combatantTwoPrefab;
    [SerializeField] private SlammableTarget slammableTargetPrefab;

    [Header("Spawn Properties")]
    [SerializeField] private Transform spawnCentreTransform;
    [SerializeField] private float spawnPositionHeight;
    [SerializeField] private float spawnAreaRadius;
    [SerializeField] private Vector2 spawnDistanceDifferenceAllowedRange = new Vector2(1.5f, 5f);
    [SerializeField] private float maximumDistancingAttemptCount = 10f;

    [Header("Camera Focus Properties")]
    [SerializeField] private CameraFocusPosition cameraFocuser;

    private List<GameObject> activeGameObjects;

    private int remainingSlammableTargetCount;

    [Header("Screen Displays")]
    [SerializeField] private GameObject tutorialScreenGameObject;
    [SerializeField] private GameObject pVEScreenGameObject;
    [SerializeField] private GameObject pVPScreenGameObject;
    [SerializeField] private GameObject pVAIScreenGameObject;
    [SerializeField] private GameObject aIVsAIScreenGameObject;
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
        InitializeTutorial();
    }

    public void InitializeTutorial()
    {
        matchEndScreenGameObject.SetActive(false);

        tutorialScreenGameObject.SetActive(true);

        int numberOfTargets = 1;

        activeGameObjects = InitializeCharacterWithTargets(playerPrefab, slammableTargetPrefab, numberOfTargets);

        remainingSlammableTargetCount = numberOfTargets;
    }

    public void InitializePvE()
    {
        matchEndScreenGameObject.SetActive(false);

        pVEScreenGameObject.SetActive(true);
    }

    public void InitializePvAi()
    {
        matchEndScreenGameObject.SetActive(false);

        pVAIScreenGameObject.SetActive(true);

        activeGameObjects = InitializeCharacters(playerPrefab, combatantTwoPrefab);
    }

    public void InitializePvP()
    {
        matchEndScreenGameObject.SetActive(false);

        pVPScreenGameObject.SetActive(true);

        activeGameObjects = InitializeCharacters(playerPrefab, playerTwoPrefab);
    }

    public void InitializeAiVsAi()
    {
        matchEndScreenGameObject.SetActive(false);

        aIVsAIScreenGameObject.SetActive(true);

        activeGameObjects = InitializeCharacters(combatantOnePrefab, combatantTwoPrefab);
    }

    public void EvaluateMatchEnd(GameObject destroyedTarget)
    {
        bool isMatchEnded = false;

        if(destroyedTarget.GetComponent<Character>() != null)
        {
            isMatchEnded = true;
        }
        else if(destroyedTarget.GetComponent<SlammableTarget>() != null)
        {
            remainingSlammableTargetCount--;

            if(remainingSlammableTargetCount <= 0)
            {
                isMatchEnded = true;
            }
        }

        if (isMatchEnded)
        {
            for(int i = 0; i < activeGameObjects.Count; i++)
            {
                if (activeGameObjects[i] != null)
                {
                    Destroy(activeGameObjects[i]);
                }
            }

            activeGameObjects = null;

            if (tutorialScreenGameObject.activeSelf)
            {
                tutorialScreenGameObject.SetActive(false);
            }

            if (pVEScreenGameObject.activeSelf)
            {
                pVEScreenGameObject.SetActive(false);
            }

            if (pVPScreenGameObject.activeSelf)
            {
                pVPScreenGameObject.SetActive(false);
            }

            if (pVAIScreenGameObject.activeSelf)
            {
                pVAIScreenGameObject.SetActive(false);
            }

            if (aIVsAIScreenGameObject.activeSelf)
            {
                aIVsAIScreenGameObject.SetActive(false);
            }

            matchEndScreenGameObject.SetActive(true);
        }
    }

    private List<GameObject> InitializeCharacters(Character characterOnePrefab, Character characterTwoPrefab)
    {
        List<GameObject> initializedGameObjectsList = new List<GameObject>();

        Vector2 randomPointInCircleOfSpawnAreaRadius = Random.insideUnitCircle * spawnAreaRadius;

        Vector3 spawnPoint = spawnCentreTransform.position + new Vector3(randomPointInCircleOfSpawnAreaRadius.x, spawnPositionHeight, randomPointInCircleOfSpawnAreaRadius.y);

        Character characterOne = Instantiate(characterOnePrefab, spawnPoint, characterOnePrefab.transform.rotation);

        initializedGameObjectsList.Add(characterOne.gameObject);

        Vector2 secondRandomPointInCircleOfSpawnAreaRadius;

        int distancingAttempts = 0;

        float spawnMinimumDistanceDifferenceSquared = spawnDistanceDifferenceAllowedRange.x * spawnDistanceDifferenceAllowedRange.x;

        float spawnMaximumDistanceDifferenceSquared = spawnDistanceDifferenceAllowedRange.y * spawnDistanceDifferenceAllowedRange.y;

        float distanceApartSquared;

        do
        {
            secondRandomPointInCircleOfSpawnAreaRadius = Random.insideUnitCircle * spawnAreaRadius;

            distanceApartSquared = (randomPointInCircleOfSpawnAreaRadius - secondRandomPointInCircleOfSpawnAreaRadius).sqrMagnitude;

            distancingAttempts++;
        }
        while ((distanceApartSquared < spawnMinimumDistanceDifferenceSquared || distanceApartSquared > spawnMaximumDistanceDifferenceSquared) && distancingAttempts <= maximumDistancingAttemptCount);

        Vector3 secondSpawnPoint = spawnCentreTransform.position + new Vector3(secondRandomPointInCircleOfSpawnAreaRadius.x, spawnPositionHeight, secondRandomPointInCircleOfSpawnAreaRadius.y);

        Character characterTwo = Instantiate(characterTwoPrefab, secondSpawnPoint, characterTwoPrefab.transform.rotation);

        initializedGameObjectsList.Add(characterTwo.gameObject);

        cameraFocuser.SetFocusTargetTranforms(characterOne.transform, characterTwo.transform);

        if(characterOne.TryGetComponent(out CombatantAI characterOneCombatantAI))
        {
            characterOneCombatantAI.SetTarget(characterTwo);
        }

        if(characterTwo.TryGetComponent(out CombatantAI characterTwoCombatantAI))
        {
            characterTwoCombatantAI.SetTarget(characterOne);
        }

        return initializedGameObjectsList;
    }

    private List<GameObject> InitializeCharacterWithTargets(Character characterPrefab, SlammableTarget targets, int targetNumber)
    {
        List<GameObject> initializedGameObjectsList = new List<GameObject>();

        Vector2 randomPointInCircleOfSpawnAreaRadius = Random.insideUnitCircle * spawnAreaRadius;

        Vector3 spawnPoint = spawnCentreTransform.position + new Vector3(randomPointInCircleOfSpawnAreaRadius.x, spawnPositionHeight, randomPointInCircleOfSpawnAreaRadius.y);

        Character character = Instantiate(characterPrefab, spawnPoint, characterPrefab.transform.rotation);

        initializedGameObjectsList.Add(character.gameObject);

        //WIP
        Vector2 secondRandomPointInCircleOfSpawnAreaRadius;

        int distancingAttempts = 0;

        float spawnMinimumDistanceDifferenceSquared = spawnDistanceDifferenceAllowedRange.x * spawnDistanceDifferenceAllowedRange.x;

        float spawnMaximumDistanceDifferenceSquared = spawnDistanceDifferenceAllowedRange.y * spawnDistanceDifferenceAllowedRange.y;

        float distanceApartSquared;

        do
        {
            secondRandomPointInCircleOfSpawnAreaRadius = Random.insideUnitCircle * spawnAreaRadius;

            distanceApartSquared = (randomPointInCircleOfSpawnAreaRadius - secondRandomPointInCircleOfSpawnAreaRadius).sqrMagnitude;

            distancingAttempts++;
        }
        while ((distanceApartSquared < spawnMinimumDistanceDifferenceSquared || distanceApartSquared > spawnMaximumDistanceDifferenceSquared) && distancingAttempts <= maximumDistancingAttemptCount);

        Vector3 secondSpawnPoint = spawnCentreTransform.position + new Vector3(secondRandomPointInCircleOfSpawnAreaRadius.x, spawnPositionHeight, secondRandomPointInCircleOfSpawnAreaRadius.y);

        SlammableTarget newTarget = Instantiate(slammableTargetPrefab, secondSpawnPoint, slammableTargetPrefab.transform.rotation);

        initializedGameObjectsList.Add(newTarget.gameObject);

        cameraFocuser.SetFocusTargetTranforms(character.transform, newTarget.transform);

        return initializedGameObjectsList;
    }
}

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

    [Header("Component References")]
    [SerializeField] private CameraFocusPosition cameraFocuser;
    [SerializeField] private GameObject matchEndScreenGameObject;

    [Header("Mode Properties")]
    [Min(1)]
    [SerializeField] private int targetsToSpawnInPVEMode;

    [Header("Spawn Properties")]
    [SerializeField] private Transform spawnCentreTransform;
    [SerializeField] private float spawnPositionHeight;
    [SerializeField] private float spawnAreaRadius;
    [SerializeField] private Vector2 spawnDistanceDifferenceAllowedRange = new Vector2(1.5f, 5f);
    [SerializeField] private float maximumDistancingAttemptCount = 10f;

    [Header("Weapon Pickup Properties")]
    [SerializeField] private SpawnWeaponPickupSystem spawnPickupSystem;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource roundStartSound;
    [SerializeField] private AudioSource roundEndSound;

    private List<GameObject> activeGameObjects;
    private int remainingSlammableTargetCount;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private void OnEnable()
    {
        PlayerInputHandler.Instance.OnChangeTargetInput += FocusOnAnotherTarget;
    }

    private void OnDisable()
    {
        PlayerInputHandler.Instance.OnChangeTargetInput -= FocusOnAnotherTarget;
    }

    public void InitializeTutorial()
    {
        InitializeNewMatch();

        activeGameObjects = InitializeCharacterWithTargets(playerPrefab, slammableTargetPrefab, 1);
    }

    public void InitializePvE()
    {
        InitializeNewMatch();

        activeGameObjects = InitializeCharacterWithTargets(playerPrefab, slammableTargetPrefab, targetsToSpawnInPVEMode);
    }

    public void InitializePvAi()
    {
        InitializeNewMatch();

        activeGameObjects = InitializeCharacters(playerPrefab, combatantTwoPrefab);
    }

    public void InitializePvP()
    {
        InitializeNewMatch();

        activeGameObjects = InitializeCharacters(playerPrefab, playerTwoPrefab);
    }

    public void InitializeAiVsAi()
    {
        InitializeNewMatch();

        activeGameObjects = InitializeCharacters(combatantOnePrefab, combatantTwoPrefab);
    }

    public void EvaluateMatchEnd(GameObject destroyedTarget)
    {
        bool isMatchEnded = false;

        activeGameObjects.Remove(destroyedTarget);

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
            else
            {
                if(cameraFocuser.TransformTwo == destroyedTarget.transform)
                {
                    for(int i = 0; i < activeGameObjects.Count; i++)
                    {
                        if (activeGameObjects[i] != cameraFocuser.TransformOne.gameObject)
                        {
                            cameraFocuser.SetTransformTwo(activeGameObjects[i].transform);

                            break;
                        }
                    }
                }
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

            spawnPickupSystem.ClearDroppedWeapons();

            roundEndSound.Play();

            matchEndScreenGameObject.SetActive(true);
        }
    }

    private List<GameObject> InitializeCharacters(Character characterOnePrefab, Character characterTwoPrefab)
    {
        List<GameObject> initializedGameObjectsList = new List<GameObject>();

        Vector2 randomPointInCircleOfSpawnAreaRadius = Random.insideUnitCircle * spawnAreaRadius;

        Vector3 spawnPoint = new Vector3(spawnCentreTransform.position.x + randomPointInCircleOfSpawnAreaRadius.x, spawnPositionHeight, spawnCentreTransform.position.z + randomPointInCircleOfSpawnAreaRadius.y);

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

        Vector3 secondSpawnPoint = new Vector3(spawnCentreTransform.position.x + secondRandomPointInCircleOfSpawnAreaRadius.x, spawnPositionHeight, spawnCentreTransform.position.z + secondRandomPointInCircleOfSpawnAreaRadius.y);

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

        Vector3 spawnPoint = new Vector3(spawnCentreTransform.position.x + randomPointInCircleOfSpawnAreaRadius.x, spawnPositionHeight, spawnCentreTransform.position.z + randomPointInCircleOfSpawnAreaRadius.y);

        Character character = Instantiate(characterPrefab, spawnPoint, characterPrefab.transform.rotation);

        initializedGameObjectsList.Add(character.gameObject);

        Vector2 secondRandomPointInCircleOfSpawnAreaRadius;

        int distancingAttempts = 0;

        float spawnMinimumDistanceDifferenceSquared = spawnDistanceDifferenceAllowedRange.x * spawnDistanceDifferenceAllowedRange.x;

        float spawnMaximumDistanceDifferenceSquared = spawnDistanceDifferenceAllowedRange.y * spawnDistanceDifferenceAllowedRange.y;

        float distanceApartSquared;

        for (int i = 0; i < targetNumber; i++)
        {
            do
            {
                secondRandomPointInCircleOfSpawnAreaRadius = Random.insideUnitCircle * spawnAreaRadius;

                distanceApartSquared = (randomPointInCircleOfSpawnAreaRadius - secondRandomPointInCircleOfSpawnAreaRadius).sqrMagnitude;

                distancingAttempts++;
            }
            while ((distanceApartSquared < spawnMinimumDistanceDifferenceSquared || distanceApartSquared > spawnMaximumDistanceDifferenceSquared) && distancingAttempts <= maximumDistancingAttemptCount);

            Vector3 secondSpawnPoint = new Vector3(spawnCentreTransform.position.x + secondRandomPointInCircleOfSpawnAreaRadius.x, spawnPositionHeight, spawnCentreTransform.position.z + secondRandomPointInCircleOfSpawnAreaRadius.y);

            SlammableTarget newTarget = Instantiate(slammableTargetPrefab, secondSpawnPoint, slammableTargetPrefab.transform.rotation);

            initializedGameObjectsList.Add(newTarget.gameObject);
        }

        remainingSlammableTargetCount = targetNumber;

        cameraFocuser.SetFocusTargetTranforms(character.transform, initializedGameObjectsList[1].transform);

        return initializedGameObjectsList;
    }

    private void FocusOnAnotherTarget()
    {
        if(remainingSlammableTargetCount <= 1)
        {
            return;
        }

        GameObject currentTarget = cameraFocuser.TransformTwo.gameObject;

        List<int> otherTargetIndexes = new List<int>();

        for(int i = 0; i < activeGameObjects.Count; i++)
        {
            if (activeGameObjects[i].GetComponent<Character>() != null || activeGameObjects[i] == currentTarget)
            {
                continue;
            }

            otherTargetIndexes.Add(i);
        }

        int newRandomTargetIndex = Random.Range(0, otherTargetIndexes.Count);

        cameraFocuser.SetTransformTwo(activeGameObjects[otherTargetIndexes[newRandomTargetIndex]].transform);
    }

    private void InitializeNewMatch()
    {
        matchEndScreenGameObject.SetActive(false);

        spawnPickupSystem.SpawnDroppedPickups();

        roundStartSound.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchMode { PostSinking, WhackACombatant, PeerReview }
public class MatchHandler : MonoBehaviour
{
    public static MatchHandler Instance;

    private List<GameObject> activeTargets;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void InitializeMatch(MatchMode matchMode)
    {
        switch (matchMode)
        {
            case MatchMode.PostSinking:
                break;
            case MatchMode.WhackACombatant:
                break;
            case MatchMode.PeerReview:
                break;
        }
    }

    public void EvaluateMatchEnd()
    {

    }
}

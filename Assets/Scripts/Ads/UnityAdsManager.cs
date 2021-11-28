using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour
{
    [SerializeField] private bool testMode = true;
    private string gameId = "3786103";

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent playerConnected;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        bool connected = false;
        PlayerPrefs.DeleteKey("LootLockerGuestPlayerID");
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.LogError("Error starting LootLocker guest session");
                Debug.LogError(response.text);
                return;
            }
            Debug.Log("Successfully started lootLocker session");
            connected = true;
        });
        yield return new WaitUntil(() => connected);
        playerConnected.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

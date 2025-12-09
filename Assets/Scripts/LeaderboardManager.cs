using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public TMP_Text leaderboardText;        // UI-teksti
    public int maxEntries = 5;          // Kuinka monta pelaajaa näytetään
    public float updateInterval = 0.5f; // Kuinka usein lista päivittyy

    private void Start()
    {
        InvokeRepeating(nameof(UpdateLeaderboard), 0f, updateInterval);
    }

    void UpdateLeaderboard()
    {
        // HAE KAIKKI PELAAT JA VIHOLLISET
        List<GameObject> all = new List<GameObject>();

        all.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        all.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        // JÄRJESTÄ SUURIMMAN KOON MUKAAN
        all = all.OrderByDescending(o => o.transform.localScale.x).ToList();

        // VAIN TOP 5
        var top = all.Take(maxEntries).ToList();

        // RAKENNA NÄYTETTÄVÄ TEKSTI
        string result = "<b>Leaderboard</b>\n\n";

        int rank = 1;
        foreach (var obj in top)
        {
            float size = obj.transform.localScale.x;
            string name = obj.name;

            result += $"{rank}. {name} - {size:F1}\n";
            rank++;
        }

        leaderboardText.text = result;
    }
}

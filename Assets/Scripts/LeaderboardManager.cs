using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using System.Threading.Tasks;

public class LeaderboardManager : MonoBehaviour
{
    public string leaderboardId = "TarekTemplate2D";
    public GameObject loadingScreen;
    public GameObject failedScreen;
    public GameObject listScreen;
    public Transform listParent;
    public GameObject entryPrefab;
    public LeaderboardEntryUI playerEntryUI;
    public Sprite[] rankSprites;
    public Sprite[] countriesInspectorSprites;
    private List<CountryData> countries = new List<CountryData>();
    Dictionary<int, Sprite> countrySprites = new Dictionary<int, Sprite>();
    public GameObject textInLeaderboard;
    public string currentName = "Unnamed";
    public int currentCountry = 0;
    public int currentScore = 0;
    public string[] Tiers;

    [System.Serializable]
    public class ScoreMetadata
    {
        public int countryId;
        public string playerName;
    }

    public void SetAllLeaderboardData(string _name, int _score, int _country)
    {
        PlayerPrefs.SetString("Player", _name);
        PlayerPrefs.SetInt("Score", _score);
        PlayerPrefs.SetInt("Country", _country);
    }
    // Start is called before the first frame update
    async void Start()
    {
        SetAllLeaderboardData(currentName, currentScore, currentCountry);

        loadingScreen.SetActive(true);
        failedScreen.SetActive(false);
        listScreen.SetActive(false);
        countries.Clear();
        for (int i = 0; i < countriesInspectorSprites.Length; i++)
        {
            CountryData CurrentCountry = new CountryData { id = i, sprite = countriesInspectorSprites[i] };
            countries.Add(CurrentCountry);
        }
        foreach (var country in countries)
            countrySprites[country.id] = country.sprite;
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        await AddScoreWithCountry();

        await LoadLeaderboard();
    }

    public async Task AddScoreWithCountry()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        string savedName = PlayerPrefs.GetString("Player", "Unnamed");
        int countryId = PlayerPrefs.GetInt("Country", 0);

        await LeaderboardsService.Instance.AddPlayerScoreAsync(
            leaderboardId,
            score,
            new AddPlayerScoreOptions
            {
                Metadata = new Dictionary<string, object>
                {
                    { "countryId", countryId },
                    { "playerName", savedName }
                }
            }
        );

    }

    public async Task LoadLeaderboard()
    {
        try
        {
            var scores = await LeaderboardsService.Instance.GetScoresAsync(
                leaderboardId,
                new GetScoresOptions { Limit = 50, IncludeMetadata = true }
            );

            if (scores == null || scores.Results == null)
            {
                Debug.LogWarning("Leaderboard: scores or results null.");
                loadingScreen?.SetActive(false);
                listScreen?.SetActive(true);
                return;
            }

            if (listParent == null)
            {
                Debug.LogError("Leaderboard: listParent is null.");
                loadingScreen?.SetActive(false);
                failedScreen?.SetActive(true);
                return;
            }

            foreach (Transform child in listParent)
            {
                bool keepPlayer = playerEntryUI != null && child.gameObject == playerEntryUI.gameObject;
                bool keepText = textInLeaderboard != null && child.gameObject == textInLeaderboard;
                if (!keepPlayer && !keepText) Destroy(child.gameObject);
            }

            void ParseMetadata(object metaObj, out int outCountryId, out string outPlayerName)
            {
                outCountryId = 0;
                outPlayerName = null;
                if (metaObj == null) return;

                if (metaObj is string s)
                {
                    if (string.IsNullOrEmpty(s) || s == "null") return;
                    try
                    {
                        var parsed = JsonUtility.FromJson<ScoreMetadata>(s);
                        if (parsed != null)
                        {
                            outCountryId = parsed.countryId;
                            outPlayerName = parsed.playerName;
                        }
                        return;
                    }
                    catch { }
                }

                try
                {
                    var dict = metaObj as System.Collections.IDictionary;
                    if (dict != null)
                    {
                        if (dict.Contains("countryId") && dict["countryId"] != null)
                            outCountryId = System.Convert.ToInt32(dict["countryId"]);
                        if (dict.Contains("playerName") && dict["playerName"] != null)
                            outPlayerName = dict["playerName"].ToString();
                        return;
                    }
                }
                catch { }

                try
                {
                    string s2 = metaObj.ToString();
                    if (!string.IsNullOrEmpty(s2) && s2 != "null")
                    {
                        var parsed = JsonUtility.FromJson<ScoreMetadata>(s2);
                        if (parsed != null)
                        {
                            outCountryId = parsed.countryId;
                            outPlayerName = parsed.playerName;
                        }
                    }
                }
                catch { }
            }

            foreach (var score in scores.Results)
            {
                try
                {
                    if (score == null) continue;
                    if (entryPrefab == null) continue;

                    var obj = Instantiate(entryPrefab, listParent);
                    var ui = obj.GetComponent<LeaderboardEntryUI>();
                    if (ui == null)
                    {
                        Destroy(obj);
                        continue;
                    }

                    Sprite rankSprite = null;
                    if (Tiers != null && rankSprites != null)
                    {
                        for (int i = 0; i < Tiers.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(score.Tier) && score.Tier == Tiers[i] && i < rankSprites.Length)
                            {
                                rankSprite = rankSprites[i];
                                break;
                            }
                        }
                    }

                    int countryId = 0;
                    string fallbackName = !string.IsNullOrEmpty(score.PlayerName) ? score.PlayerName : "Unnamed";
                    string displayName = PlayerPrefs.GetString("Player", fallbackName ?? "Unnamed");

                    int metaCountry = 0;
                    string metaName = null;
                    ParseMetadata(score.Metadata, out metaCountry, out metaName);
                    if (metaCountry != 0) countryId = metaCountry;
                    if (!string.IsNullOrEmpty(metaName)) displayName = metaName;

                    Sprite countrySprite = null;
                    if (countrySprites != null && countrySprites.ContainsKey(countryId))
                        countrySprite = countrySprites[countryId];

                    ui.SetData(rankSprite, countrySprite, displayName ?? "Unnamed", score.Score, score.Rank + 1);
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning("Failed processing a leaderboard entry: " + e);
                }

            }

            var playerScore = await LeaderboardsService.Instance.GetPlayerScoreAsync(
                leaderboardId,
                new GetPlayerScoreOptions { IncludeMetadata = true }
            );

            if (playerScore != null)
            {
                bool inTop = false;
                foreach (var s in scores.Results)
                {
                    if (s != null && s.PlayerId == playerScore.PlayerId)
                    {
                        inTop = true;
                        break;
                    }
                }

                if (!inTop)
                {
                    int countryId = 0;
                    string displayName = PlayerPrefs.GetString("Player", "Unnamed");

                    ParseMetadata(playerScore.Metadata, out int metaCountry, out string metaName);
                    if (metaCountry != 0) countryId = metaCountry;
                    if (!string.IsNullOrEmpty(metaName)) displayName = metaName;

                    Sprite rankSprite = null;
                    if (Tiers != null && rankSprites != null)
                    {
                        for (int i = 0; i < Tiers.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(playerScore.Tier) && playerScore.Tier == Tiers[i] && i < rankSprites.Length)
                            {
                                rankSprite = rankSprites[i];
                                break;
                            }
                        }
                    }

                    Sprite countrySprite = null;
                    if (countrySprites != null && countrySprites.ContainsKey(countryId))
                        countrySprite = countrySprites[countryId];

                    if (playerEntryUI != null)
                    {
                        playerEntryUI.SetData(rankSprite, countrySprite, displayName, playerScore.Score, playerScore.Rank + 1);
                        playerEntryUI.gameObject.SetActive(true);
                    }
                }
                else if (playerEntryUI != null) playerEntryUI.gameObject.SetActive(false);
            }

            loadingScreen?.SetActive(false);
            listScreen?.SetActive(true);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Leaderboard error: " + ex.ToString());
            loadingScreen?.SetActive(false);
            failedScreen?.SetActive(true);
        }
    }


}

[System.Serializable]
public class CountryData
{
    public int id;
    public Sprite sprite;
}
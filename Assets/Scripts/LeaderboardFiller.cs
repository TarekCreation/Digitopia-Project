using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LeaderboardFiller : MonoBehaviour
{
    public string leaderboardId = "DigitopiaProject";

    List<string> exampleNames = new List<string>
    {
        "Mohamed", "Mo2men", "AliGamer", "A7med", "Tarek",
        "Player", "User1264", "Kamal", "Yassin", "Yasser",
        "Ramez", "Raed", "Medhat", "Yousuf", "Mo7sen",
        "Mohamed", "محمد", "أشرف", "كريم", "شريف",
        "مدحت", "فادي", "مازن", "Fighter", "Kamal634",
        "Account", "Player33242", "TarekCreations", "Doha564", "User3576",
        "MalwareMash", "User423", "عبدالله", "يزن423", "نبيل",
        "Player112", "AhmedMohamed", "طارق241", "عبدالرحمن", "عبدالمجيد",
        "234مدحت", "Salah", "محمد_4423", "كمال", "عبده532",
        "User3234", "Ashraf", "Gamer32", "سيد", "حازم"
    };

    async void Start()
    {
        await UnityServices.InitializeAsync();
        await CreateExamplePlayers();
    }

    async Task CreateExamplePlayers()
    {
        for (int i = 0; i < exampleNames.Count; i++)
        {
            AuthenticationService.Instance.SignOut();
            AuthenticationService.Instance.ClearSessionToken();

            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            int score = Random.Range(10, 6000);
            int countryId = Random.Range(0, 79);

            var metadata = new Dictionary<string, object>
            {
                { "playerName", exampleNames[i] },
                { "countryId", countryId }
            };

            await LeaderboardsService.Instance.AddPlayerScoreAsync(
                leaderboardId,
                score,
                new AddPlayerScoreOptions { Metadata = metadata }
            );

            Debug.Log($"Added {exampleNames[i]} with score {score}");
        }
    }
}

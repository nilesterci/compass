using Newtonsoft.Json;
using RestSharp;
using System.Net;

public class Program
{
    public class Datum
    {
        public string competition { get; set; }
        public int year { get; set; }
        public string round { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string team1goals { get; set; }
        public string team2goals { get; set; }
    }

    public class DataResponse
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Datum> data { get; set; }
    }

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year, int page = 1, int totalGoals = 0, string type = "team1")
    {
        try
        {
            int totalGoalsPage = 0;
            string typeTeam = type;
            var client = new RestClient("https://jsonmock.hackerrank.com/api/football_matches");

            var request = new RestRequest("", Method.Get);
            request.AddQueryParameter(type, team);
            request.AddQueryParameter("year", year);
            request.AddQueryParameter("page", page);

            var queryResult = client.Execute<DataResponse>(request).Data;

            foreach (var item in queryResult.data)
            {
                if (type == "team1")
                    totalGoalsPage += Convert.ToInt32(item.team1goals);
                else
                    totalGoalsPage += Convert.ToInt32(item.team2goals);
            }

            if (page == queryResult.total_pages)
            {
                if (type == "team2")
                {
                    return totalGoals + totalGoalsPage;

                }
                else
                {
                    typeTeam = "team2";
                    totalGoals = totalGoalsPage + totalGoals;
                    return getTotalScoredGoals(team, year, page = 1, totalGoals, typeTeam);
                }
            }
            else
            {
                totalGoals = totalGoalsPage + totalGoals;
                return getTotalScoredGoals(team, year, page + 1, totalGoals, typeTeam);
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

}


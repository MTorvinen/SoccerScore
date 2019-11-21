using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoreService {
    public class ResultGroup {
        public DateTime MatchDate { get; set; }
        public List<GameResult> Results { get; set; }

        public ResultGroup(DateTime matchDate, IEnumerable<GameResult> results) {
            MatchDate = matchDate;
            Results = results.ToList();
        }

    }

    public static class ResultGroupExtensions {

        public static IEnumerable<ResultGroup> FilterByTeam(this IEnumerable<ResultGroup> groups, string team) {
            foreach (var group in groups) {
                var results = group.Results.FilterByTeam(team);
                if (results.Any()) {
                    yield return new ResultGroup(group.MatchDate, group.Results.FilterByTeam(team));
                }                
            }            
        }

        public static IEnumerable<GameResult> FilterByTeam(this IEnumerable<GameResult> results, string team) {
            team = team.ToLower();
            return results.Where(f => f.HomeTeam.Name.ToLower().Contains(team) || f.AwayTeam.Name.ToLower().Contains(team));
        }

    }

}

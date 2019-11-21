using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScoreService{

    public interface IVeikkausliigaService {
        Task<GameResult> GetResultAsync(int id);
        Task<IList<GameResult>> GetResultsAsync();
        Task<IEnumerable<ResultGroup>> GetResultGroups();
    }

    public class VeikkausliigaService : IVeikkausliigaService {

        private const string ApiUrl = "https://functionapp2018071101324.blob.core.windows.net/data/matches_latest.json";

        private IList<GameResult> _results;

        public async Task<IList<GameResult>> GetResultsAsync() {
            if (_results == null || !_results.Any()) {
                using (var client = new HttpClient() { Timeout = new System.TimeSpan(0, 0, 30) }) {
                    try {
                        var json = await client.GetStringAsync(ApiUrl).ConfigureAwait(false);
                        if (!string.IsNullOrWhiteSpace(json)) {
                            _results = JsonSerializer.Deserialize<IList<GameResult>>(json,
                                new JsonSerializerOptions { AllowTrailingCommas = true }
                            );
                        }
                    } catch (WebException ex) {
                        var m = ex.Message;
                    }
                }
            }

            return _results;
        }

        public async Task<GameResult> GetResultAsync(int id) {
            var results = await GetResultsAsync();
            return results.FirstOrDefault(f => f.Id == id);
        }

        public async Task<IEnumerable<ResultGroup>> GetResultGroups() {
            var results = await GetResultsAsync();
            return results.GroupBy(f => f.MatchDate).Select(f => new ResultGroup(f.Key, f.ToList()));
        }

    }
}

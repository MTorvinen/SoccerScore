using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
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
        public string ApiUrl { get; }
    }

    public class VeikkausliigaService : IVeikkausliigaService {

        public VeikkausliigaService() {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            if (File.Exists(path)) {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile(path, false);
                var config = configurationBuilder.Build();
                ApiUrl = config["ApiUrl"];
            }
        }

        public string ApiUrl { get; private set; }

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

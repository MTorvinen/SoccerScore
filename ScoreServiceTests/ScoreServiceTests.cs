using NUnit.Framework;
using ScoreService;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ScoreServiceTests{
    public class VeikkausliigaServiceTests{

        private IVeikkausliigaService Service = new VeikkausliigaService();        

        [Test]
        public async Task GetResultsAsync(){
            var results = await Service.GetResultsAsync();
            Assert.NotNull(results);
            Assert.Greater(results.Count(), 0);
        }

        [Test]
        public async Task HasDatesAsync() {
            var results = await Service.GetResultsAsync();
            var datemissing = results.Any(f => f.MatchDate == DateTime.MinValue);
            Assert.False(datemissing);
        }

        [Test]
        public async Task HasNamesAsync() {
            var results = await Service.GetResultsAsync();
            var namesmissing = results.Any(f => string.IsNullOrWhiteSpace(f.HomeTeam?.Name) || string.IsNullOrWhiteSpace(f.AwayTeam?.Name));
            Assert.False(namesmissing);
        }

    }
}
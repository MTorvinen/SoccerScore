using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreService{

    public class GameResult{
        public int Id { get; set; }       
        public int RoundNumber { get; set; }
        public DateTime MatchDate { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public int Status { get; set; }
        public int PlayedMinutes { get; set; }        
        public DateTime? GameStarted { get; set; }
        public IList<MatchEvent> MatchEvents { get; set; }
        public bool OnlyResultAvailable { get; set; }
        public int Season { get; set; }
        public string Country { get; set; }
        public string League { get; set; }

        public object Round { get; set; }
        public IList<object> PeriodResults { get; set; }
        public object SecondHalfStarted { get; set; }
    }

}

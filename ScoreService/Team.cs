using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreService{
    public class Team{
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public object Logo { get; set; }
        public string LogoUrl { get; set; }
        public int Ranking { get; set; }
        public string Message { get; set; }
    }
}

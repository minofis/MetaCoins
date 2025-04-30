using System.Text.Json.Serialization;
using MetaCoins.Core.Entities.Lookups.Votes;

namespace MetaCoins.Core.Entities.Votes
{
    public class VotingSession
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public int VotingTypeId { get; set; }
        public VotingType VotingType { get; set; } = null!;
        
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid? ParentVotingSessionId { get; set; }
        public VotingSession? ParentVotingSession { get; set; }
        public ICollection<VotingSession> SubVotingSessions { get; set; } = new List<VotingSession>();

        public ICollection<CoinVote> CoinVotes { get; set; } = new List<CoinVote>();

        public Guid? WinnerId { get; set; }
        public Coin? Winner { get; set; }

        [JsonIgnore]
        public ICollection<CoinVotingSession> CoinVotingSessions { get; set; } = new List<CoinVotingSession>();
    }
}
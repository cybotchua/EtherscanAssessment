using System;
using System.Collections.Generic;

namespace EtherscanAssessment.Models.Tokens
{
    public partial class Token
    {
        public string Id { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public string ContractAddress { get; set; }

        public int TotalSupply { get; set; }

        public int TotalHolders { get; set; }
    }
}
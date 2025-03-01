﻿namespace BridgeRTU.Services.Activities.Dto
{
    public class UpdateActivityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public List<int> InterestIds { get; set; }  // Interest IDs to update the association
    }

}

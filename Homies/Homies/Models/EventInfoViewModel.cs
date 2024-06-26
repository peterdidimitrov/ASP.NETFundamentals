﻿using Homies.Constants;

namespace Homies.Models
{
    public class EventInfoViewModel
    {
        public EventInfoViewModel(int id, string name, DateTime startingTime, string type, string organiser)
        {
            Id = id;
            Name = name;
            Start = startingTime.ToString(EventConstants.EventDateFormat);
            Type = type;
            Organiser = organiser;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Start { get; set; }

        public string Type { get; set; }

        public string Organiser { get; set; }
    }
}

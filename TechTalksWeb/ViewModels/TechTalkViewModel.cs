using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechTalksModel.DTO;

namespace TechTalksWeb.ViewModels
{
    public class TechTalkViewModel
    {
        public string TechTalkName { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Meetup" },
            new SelectListItem { Value = "2", Text = "Free Conference" },
            new SelectListItem { Value = "3", Text = "Paid Conference"  },
            new SelectListItem { Value = "4", Text = "Hackathon"  },
            new SelectListItem { Value = "5", Text = "Eventribe"  },
        };
    }
}
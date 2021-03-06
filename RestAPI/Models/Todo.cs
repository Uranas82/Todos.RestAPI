using Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Models
{
    public class Todo
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Difficulty Difficulty { get; set; }

        public bool IsDone { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Models.RequestModels
{
    public class UpdateTodoStatusRequest
    {
        public bool IsDone { get; set; }
    }
}

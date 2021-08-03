using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class NationalParkDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EstablishedAt { get; set; }
    }
}

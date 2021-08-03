using API.Models;
using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class NationalParkRepository : INationalParkRepository
    {
        /// <summary>
        /// Creates a national park
        /// </summary>
        /// <param name="nationalPark"></param>
        /// <returns></returns>
        public bool CreateNationlPark(NationalPark nationalPark)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            throw new NotImplementedException();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            throw new NotImplementedException();
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            throw new NotImplementedException();
        }

        public bool IsNationalParkExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsNationalParkExists(string name)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            throw new NotImplementedException();
        }
    }
}

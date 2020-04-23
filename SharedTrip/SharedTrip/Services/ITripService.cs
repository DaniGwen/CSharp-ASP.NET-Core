using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripService
    {
        void AddTrip(CreateTripViewModel model);
    }
}

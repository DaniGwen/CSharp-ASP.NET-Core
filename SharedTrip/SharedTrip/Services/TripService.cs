using SharedTrip.Models;
using SharedTrip.ViewModels.TripViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext context;

        public TripService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddTrip(CreateTripViewModel model)
        {
            var trip = new Trip
            {
                DepartureTime = model.DepartureTime,
                Description = model.Description,
                EndPoint = model.EndPoint,
                ImagePath = model.ImagePath,
                StartPoint = model.StartPoint,
                Seats = model.Seats,
            };

            this.context.Trips.Add(trip);
            this.context.SaveChanges();
        }

        public void CreateUserTrip(string tripId, string userId)
        {
            var trip = this.context.Trips.Find(tripId);

            if (trip.Seats <= 0)
            {
                return;
            }

            var userTrip = new UserTrip
            {
                 TripId = trip.Id,
                 UserId = userId,
            };

            trip.Seats -= 1;

            this.context.UserTrips.Add(userTrip);
            this.context.SaveChanges();
        }

        public Trip GetTripById(string id)
        {
            return this.context.Trips.Find(id);
        }

        public IQueryable<Trip> GetTrips()
        {
            return this.context.Trips;
        }

        public bool IsUserJoined(string userId, string tripId)
        {
            return this.context.UserTrips
                .Any(x => x.TripId == tripId && x.UserId == userId);
        }
    }
}

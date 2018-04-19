using System.Collections.Generic;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceTest
    {
        [Test]
        public void Should_not_read_trips_when_user_is_not_logged_in()
        {
            User.User loggedUser = null;
            var tripService = new TestableTripService(loggedUser);

            Assert.That(() => tripService.GetTripsByUser(null),
               Throws.TypeOf<UserNotLoggedInException>());
        }

        [Test]
        public void Should_return_empty_list_of_trips_when_logged_user_is_not_a_friend_of_another_user()
        {
            var loggedUser = new User.User();
            var anotherUser = new User.User();
            anotherUser.AddTrip(new Trip.Trip());
            var tripService = new TestableTripService(loggedUser);

            var trips = tripService.GetTripsByUser(anotherUser);

            CollectionAssert.IsEmpty(trips);
        }
        [Test]
        public void Should_return_list_of_trips_when_logged_user_is_a_friend_of_another_user()
        {
            var loggedUser = new User.User();
            var anotherUser = new User.User();
            var anotherUserTrip = new Trip.Trip();
            anotherUser.AddFriend(loggedUser);
            anotherUser.AddTrip(anotherUserTrip);
            var tripService = new TestableTripService(loggedUser);

            var trips = tripService.GetTripsByUser(anotherUser);

            CollectionAssert.AreEquivalent(new List<Trip.Trip> {anotherUserTrip}, trips );
           
        }
    }

    public class TestableTripService : TripService
    {
        private readonly User.User _loggedUser;

        public TestableTripService(User.User loggedUser)
        {
            _loggedUser = loggedUser;
        }

        protected override User.User GetLoggedUser()
        {
            return _loggedUser;
        }

        protected override List<Trip.Trip> TripsByUser(User.User user)
        {
            return user.Trips();
        }
}
}
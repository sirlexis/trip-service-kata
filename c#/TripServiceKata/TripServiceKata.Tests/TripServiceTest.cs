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
           

            Assert.That(trips.Count,Is.EqualTo(0));
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
    }
}
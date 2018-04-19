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
            var tripService = new TestableTripService();
            var user = new User.User();

            Assert.That(() => tripService.GetTripsByUser(user),
               Throws.TypeOf<UserNotLoggedInException>());
        }
    }

    public class TestableTripService : TripService
    {
        protected override User.User GetLoggedUser()
        {
            return null;
        }
    }
}
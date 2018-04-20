using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TripServiceKata.Tests
{
    public class UserShould
    {
        [Test]
        public void Inform_when_users_are_not_friends()
        {
            var friend = new User.User();
            var anotherUser = new User.User();

            Assert.IsFalse(friend.IsFriendsWith(anotherUser));
        }

        [Test]
        public void Inform_when_users_are_friends()
        {
            var friend = new User.User();
            var anotherUser = new User.User();

            friend.AddFriend(anotherUser);

            Assert.IsTrue(friend.IsFriendsWith(anotherUser));
        }
    }
}

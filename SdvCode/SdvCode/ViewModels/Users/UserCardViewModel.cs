﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SdvCode.ViewModels.Users
{
    public class UserCardViewModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingsCount { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public bool HasFollowed { get; set; }
    }
}
﻿// Copyright (c) SDV Code Project. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SdvCode.Models.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public class State
    {
        public State()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        // [Required]
        [ForeignKey(nameof(Country))]
        public string CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; } = new HashSet<City>();

        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();
    }
}
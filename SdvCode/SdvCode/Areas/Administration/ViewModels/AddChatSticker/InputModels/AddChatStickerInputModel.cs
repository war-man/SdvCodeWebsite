﻿// Copyright (c) SDV Code Project. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SdvCode.Areas.Administration.ViewModels.AddChatSticker.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class AddChatStickerInputModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Sticker Type")]
        public string StickerTypeId { get; set; }
    }
}
﻿// Copyright (c) SDV Code Project. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SdvCode.Areas.Administration.ViewModels.EmojiViewModels.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using SdvCode.Areas.Administration.ViewModels.EmojiViewModels.InputModels;

    public class EditEmojiBaseModel
    {
        public EditEmojiInputModel EditEmojiInputModel { get; set; } = new EditEmojiInputModel();

        public ICollection<EditEmojiViewModel> EditEmojiViewModel { get; set; } =
            new HashSet<EditEmojiViewModel>();
    }
}
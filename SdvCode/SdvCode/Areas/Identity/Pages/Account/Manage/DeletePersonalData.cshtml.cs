﻿// Copyright (c) SDV Code Project. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SdvCode.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using SdvCode.Data;
    using SdvCode.Models.Blog;
    using SdvCode.Models.User;
    using SdvCode.Services.Comment;

    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<DeletePersonalDataModel> logger;
        private readonly ApplicationDbContext db;
        private readonly ICommentService commentService;

        public DeletePersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            ApplicationDbContext db,
            ICommentService commentService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.db = db;
            this.commentService = commentService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.RequirePassword = await this.userManager.HasPasswordAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.RequirePassword = await this.userManager.HasPasswordAsync(user);
            if (this.RequirePassword)
            {
                if (!await this.userManager.CheckPasswordAsync(user, this.Input.Password))
                {
                    this.ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return this.Page();
                }
            }

            var comments = this.db.Comments.Where(x => x.ApplicationUserId == user.Id);
            foreach (var comment in comments)
            {
                await this.commentService.DeleteCommentById(comment.Id);
            }

            var posts = this.db.Posts.Where(x => x.ApplicationUserId == user.Id).ToList();
            foreach (var post in posts)
            {
                var action = this.db.UserActions.Where(x => x.PostId == post.Id).ToList();
                this.db.UserActions.RemoveRange(action);
            }

            var followFollowed = this.db.FollowUnfollows
                .Where(x => x.PersonId == user.Id || x.FollowerId == user.Id)
                .ToList();

            this.db.FollowUnfollows.RemoveRange(followFollowed);
            this.db.Posts.RemoveRange(posts);
            await this.db.SaveChangesAsync();

            var result = await this.userManager.DeleteAsync(user);
            var userId = await this.userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await this.signInManager.SignOutAsync();

            this.logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return this.Redirect("~/");
        }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
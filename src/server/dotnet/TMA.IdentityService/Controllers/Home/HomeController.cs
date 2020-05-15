// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TMA.IdentityService.Controllers.Account;
using TMA.IdentityService.Resources;

namespace TMA.IdentityService.Controllers.Home
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private const string ErrorResourcePrefix = "error_code_";
        private const string UnauthorizedErrorId = "401";
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IStringLocalizer<SR> _localizer;

        public HomeController(IIdentityServerInteractionService interaction, IStringLocalizer<SR> localizer)
        {
            _interaction = interaction ?? throw new ArgumentNullException(nameof(interaction));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Error(string errorId = null, string returnUrl = null)
        {
            if (UnauthorizedErrorId == errorId)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            var vm = new ErrorViewModel {ReturnUrl = returnUrl};
            // retrieve error details from identityserver
            var message = errorId == null ? null : await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
                message.ErrorDescription = null;
            }
            else
            {
                vm.Error = new ErrorMessage()
                {
                    Error = _localizer.GetString(ErrorResourcePrefix + errorId)
                };
            }

            return View("Error", vm);
        }

        [HttpPost]
        [ActionName("Error")]
        public IActionResult ErrorPost(string errorId = null)
        {
            return RedirectToAction("Error", new {errorId = errorId});
        }
    }
}
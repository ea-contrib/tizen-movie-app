// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;

namespace TMA.IdentityService.Controllers.Account
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = false;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
        public static bool ShowLogoutPrompt = false;
        public static bool AutomaticRedirectAfterSignOut = true;
        // specify the Windows authentication scheme being used
        public static readonly string WindowsAuthenticationSchemeName = Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        // if user uses windows auth, should we load the groups from windows
        public static bool IncludeWindowsGroups = false;

        public static string InvalidCredentialsErrorMessage = "You have entered an incorrect email address or password.";
        public static string InvalidCodeErrorMessage = "You have entered an incorrect verification code.";
        public static string SystemErrorMessage = "Unknown system error occured. Try again later or contact your system administrator.";
        public const string IncorrectEmailFormatValidation = "IncorrectEmail";
    }
}

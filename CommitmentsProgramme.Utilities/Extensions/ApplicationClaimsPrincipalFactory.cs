using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CommitmentsProgramme.Domain.Entities;
using CommitmentsProgramme.Utilities.Abstractions.Consts;
using static CommitmentsProgramme.Utilities.Abstractions.Consts.SharedData;

namespace CommitmentsProgramme.Utilities.Extensions;

public class ApplicationClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
{
    public ApplicationClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(
        ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaim(
            new Claim(
                CustomeClaims.FullName,
                user.FullName ?? ""));
        
        return identity;
    }
}
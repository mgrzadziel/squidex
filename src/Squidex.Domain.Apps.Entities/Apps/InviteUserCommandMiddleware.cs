﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System;
using System.Threading.Tasks;
using Squidex.Domain.Apps.Entities.Apps.Commands;
using Squidex.Infrastructure;
using Squidex.Infrastructure.Commands;
using Squidex.Shared.Users;

namespace Squidex.Domain.Apps.Entities.Apps
{
    public sealed class InviteUserCommandMiddleware : ICommandMiddleware
    {
        private readonly IUserResolver userResolver;

        public InviteUserCommandMiddleware(IUserResolver userResolver)
        {
            Guard.NotNull(userResolver, nameof(userResolver));

            this.userResolver = userResolver;
        }

        public async Task HandleAsync(CommandContext context, Func<Task> next)
        {
            if (context.Command is AssignContributor assignContributor)
            {
                if (assignContributor.IsInviting && assignContributor.ContributorId.IsEmail())
                {
                    var isInvited = await userResolver.CreateUserIfNotExists(assignContributor.ContributorId);

                    await next();

                    if (isInvited && context.Result<object>() is EntityCreatedResult<string> id)
                    {
                        context.Complete(new InvitedResult { Id = id });
                    }

                    return;
                }
            }

            await next();
        }
    }
}

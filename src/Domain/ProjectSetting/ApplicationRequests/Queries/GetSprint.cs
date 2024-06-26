﻿using Domainify.Domain;
using MediatR;

namespace Domain.ProjectSettingAggregation
{
    public class GetSprint :
        QueryItemRequestById<Sprint, string, SprintViewModel?>
    {
        public bool WithTasks { get; private set; } = false;
        public GetSprint(string id, 
            bool withTasks = false, 
            bool includeDeleted = false) : base(id)
        {
            WithTasks = withTasks;
            PreventIfNoEntityWasFound = true;
            IncludeDeleted = includeDeleted;
        }

        public override async Task ResolveAsync(IMediator mediator, Sprint sprint)
        {
            base.Prepare(sprint);

            await InvariantState.AssestAsync(mediator);

            await base.ResolveAsync(mediator, sprint);
        }
    }
}

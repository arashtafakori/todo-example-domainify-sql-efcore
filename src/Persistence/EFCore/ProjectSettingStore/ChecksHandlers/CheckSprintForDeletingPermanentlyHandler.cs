﻿using Domain.ProjectSettingAggregation;
using MediatR;

namespace Persistence.ProjectSettingStore
{
    public class CheckSprintForDeletingPermanentlyHandler :
        IRequestHandler<CheckSprintForDeleting>
    {
        private readonly IMediator _mediator;
        public CheckSprintForDeletingPermanentlyHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(
            CheckSprintForDeleting request,
            CancellationToken cancellationToken)
        {
            await request.ResolveAsync(_mediator);

            return new Unit();
        }
    }
}

using Application.Utility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.MovieReview
{
    public class Create
    {

        public class CreateCommand : IRequest<Result<Unit>>
        {

        }
        public class CreateHandler : IRequestHandler<CreateCommand, Result<Unit>>
        {
            public CreateHandler() { }

            public async Task<Result<Unit>> Handle(CreateCommand command,CancellationToken cancellationToken)
            {



                return
            }
        }
    }
}

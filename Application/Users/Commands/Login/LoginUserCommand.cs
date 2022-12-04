using Domain.Models;
using MediatR;

namespace Application.Users.Commands.Login;

public record LoginUserCommand(LoginUserCommandRequest Request) : IRequest<string>;

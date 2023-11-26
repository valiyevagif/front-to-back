using MediatR;

namespace Bigon.Business.Modules.AccountModule.Commands.RegisterCommand
{
    public class RegisterRequest : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

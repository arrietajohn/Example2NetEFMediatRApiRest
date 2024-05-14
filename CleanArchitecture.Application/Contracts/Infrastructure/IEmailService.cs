using MyProject.Application.Models;

namespace MyProject.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}

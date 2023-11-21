using ApplicationCore.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Identity.Features.DeleteAccount;
using Infrastructure.Identity.Features.Register;
using Infrastructure.Identity.Features.SignIn;
using Infrastructure.Identity.Features.SignOut;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Authorize]
[Route("[controller]/[action]")]
public class AccountController(IMediator mediator) : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View(new SignInCommand());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] SignInCommand signInCommand, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(signInCommand, cancellationToken);

            return RedirectToAction("Index", "Home");
        }
        catch (ValidationException e)
        {
            AddValidationErrorsToModelState(e);

            throw;
        }
        catch (AuthorizationException e)
        {
            ModelState.AddModelError("Authorization error: ", e.Message);

            throw;
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View(new RegisterCommand());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm] RegisterCommand registerCommand, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(registerCommand, cancellationToken);

            return RedirectToAction("Login");
        }
        catch (ValidationException e)
        {
            AddValidationErrorsToModelState(e);

            throw;
        }
        catch (AuthorizationException e)
        {
            ModelState.AddModelError("Authorization error: ", e.Message);

            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> SignOut(CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(new SignOutCommand(), cancellationToken);

            return RedirectToAction("Login");
        }
        catch (AuthorizationException e)
        {
            ModelState.AddModelError("Authorization error: ", e.Message);

            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAccount()
    {
        try
        {
            await mediator.Send(new DeleteAccountCommand());

            return RedirectToAction("Login");
        }
        catch (AuthorizationException e)
        {
            ModelState.AddModelError("Authorization error: ", e.Message);

            throw;
        }
    }

    private void AddValidationErrorsToModelState(ValidationException exception)
    {
        foreach (ValidationFailure? item in exception.Errors)
        {
            ModelState.AddModelError("Validation error", item.ErrorMessage);
        }
    }
}
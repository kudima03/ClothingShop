using ApplicationCore.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Identity;
using Infrastructure.Identity.Features.DeleteAccount;
using Infrastructure.Identity.Features.Register;
using Infrastructure.Identity.Features.SignIn;
using Infrastructure.Identity.Features.SignOut;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountController : Controller
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new SignInCommand());
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] SignInCommand signInCommand)
    {
        try
        {
            await _mediator.Send(signInCommand);
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
    public IActionResult Register()
    {
        return View(new RegisterCommand());
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterCommand registerCommand)
    {
        try
        {
            await _mediator.Send(registerCommand);
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
    [Authorize(Policy = PolicyName.Customer, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> SignOut()
    {
        try
        {
            await _mediator.Send(new SignOutCommand());
            return RedirectToAction("Login");
        }
        catch (AuthorizationException e)
        {
            ModelState.AddModelError("Authorization error: ", e.Message);
            throw;
        }
    }

    [HttpGet]
    [Authorize(Policy = PolicyName.Customer, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteAccount()
    {
        try
        {
            await _mediator.Send(new DeleteAccountCommand());
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
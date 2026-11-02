using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using LibraryManagement.Services.Contracts;

namespace LibraryManagement.Controllers;

[Route("api/webhooks/stripe")]
[ApiController]
public class StripeWebhookController(
    IOrderService orderService,
    IConfiguration configuration,
    ILogger<StripeWebhookController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Handle(CancellationToken cancellationToken)
    {
        var webhookSecret = configuration["Stripe:WebhookSecret"]
                            ?? Environment.GetEnvironmentVariable("STRIPE__WEBHOOK_SECRET");
        if (string.IsNullOrWhiteSpace(webhookSecret))
            return Problem("Stripe webhook is not configured.", statusCode: 500);

        using var reader = new StreamReader(Request.Body);
        var json = await reader.ReadToEndAsync(cancellationToken);
        var signature = Request.Headers["Stripe-Signature"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(signature))
            return BadRequest("Missing Stripe-Signature header.");

        Event stripeEvent;
        try
        {
            stripeEvent = EventUtility.ConstructEvent(json, signature, webhookSecret);
        }
        catch (StripeException exception)
        {
            logger.LogWarning(exception, "Invalid Stripe webhook signature.");
            return BadRequest("Invalid Stripe webhook signature.");
        }

        if (stripeEvent.Type == "checkout.session.completed")
        {
            var session = stripeEvent.Data.Object as Session;
            if (session is null)
                return BadRequest("Invalid checkout session payload.");

            var result = await orderService.CompleteCheckoutAsync(
                session.Id, session.PaymentIntentId, cancellationToken);
            if (result.IsFailed)
                return NotFound();
        }

        return Ok();
    }
}

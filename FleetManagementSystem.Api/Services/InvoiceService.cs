using System;

namespace FleetManagementSystem.Api.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IEmailService _emailService;

    public InvoiceService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public void SendInvoiceEmail(long bookingId, string email)
    {
        // Logic to generate PDF and send email would go here.
        // For now, invoking basic email or logging.
        // Java code generates PDF.
        // We will implement full logic later if strictly required right now.
        // TODO: Implement PDF generation using QuestPDF and attachment.
        
        Console.WriteLine($"Sending invoice for booking {bookingId} to {email}");
    }
}

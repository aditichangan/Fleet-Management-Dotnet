using System;
using System.Collections.Generic;
using System.Linq;
using FleetManagementSystem.Api.Data;
using FleetManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManagementSystem.Api.Services;

public class AddOnService : IAddOnService
{
    private readonly ApplicationDbContext _context;
    public AddOnService(ApplicationDbContext context) => _context = context;
    public List<AddOnMaster> GetAllAddOns() => _context.AddOns.ToList();
    public AddOnMaster AddAddOn(AddOnMaster addOn)
    {
        _context.AddOns.Add(addOn);
        _context.SaveChanges();
        return addOn;
    }
    public AddOnMaster GetAddOnById(int id) => _context.AddOns.Find(id);
    public void DeleteAddOn(int id)
    {
        var addon = _context.AddOns.Find(id);
        if (addon != null) { _context.AddOns.Remove(addon); _context.SaveChanges(); }
    }
}

public class VendorService : IVendorService
{
    private readonly ApplicationDbContext _context;
    public VendorService(ApplicationDbContext context) => _context = context;
    public List<Vendor> GetAllVendors() => _context.Vendors.ToList();
    public Vendor AddVendor(Vendor vendor)
    {
        _context.Vendors.Add(vendor);
        _context.SaveChanges();
        return vendor;
    }
    public void DeleteVendor(int id)
    {
        var vendor = _context.Vendors.Find(id);
        if (vendor != null) { _context.Vendors.Remove(vendor); _context.SaveChanges(); }
    }
}

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;
    public CustomerService(ApplicationDbContext context) => _context = context;

    public List<CustomerMaster> GetAllCustomers() => _context.Customers.ToList();
    
    public CustomerMaster GetCustomerByEmail(string email) => _context.Customers.FirstOrDefault(c => c.Email == email);
    
    public CustomerMaster AddCustomer(CustomerMaster customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public CustomerMaster UpdateCustomer(CustomerMaster customer)
    {
        var existing = _context.Customers.Find(customer.CustId);
        if (existing != null)
        {
             // Update properties logic. 
             // Simple mapping or manual update
             _context.Entry(existing).CurrentValues.SetValues(customer);
             _context.SaveChanges();
             return existing;
        }
        return null; // Or throw
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagementSystem.Api.Models;

[Table("vendors")]
public class Vendor
{
    [Key]
    [Column("vendorId")] // Java model didn't specify column name explicitly for ID, usually it's id or vendor_id, but assuming default based on field name
    public long VendorId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("type")]
    public string Type { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("apiUrl")]
    public string ApiUrl { get; set; }
}

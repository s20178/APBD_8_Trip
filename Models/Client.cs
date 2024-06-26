using System;
using System.Collections.Generic;

namespace Trip.Models;

public partial class Client
{
    public int Id { get; set; }

    public int IdClient { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Pesel { get; set; } = null!;

    public DateTime RegisteredAt { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}

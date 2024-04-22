using System.ComponentModel.DataAnnotations.Schema;
using Danilvar.Entity;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Shared.Domain.Entities;

[Owned]
public class InTest
{
    public int B { get; set; }
}

public class Test : Entity
{
    public int A { get; set; }
    public InTest InTest { get; set; }
}

using System;

namespace Catalog.Entities
{
    // Records are used instead of classes
    // Handy for immutable objects, with-expressions and value-based equalitiy comparison
    public record Item
    {
        // Introducing the properties
        // Get - init instead of get - set. We want an immutable property - after creation it can not be modified.
        public Guid Id { get; init;}
        public string Name {get; init;}
        public decimal Price {get; init;}
        public DateTimeOffset CreatedDate {get; init;}
    }
}


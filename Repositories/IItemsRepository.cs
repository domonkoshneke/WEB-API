using System;
using Catalog.Entities;
using System.Collections.Generic;

namespace Catalog.Repositories
{

    // Defining a custom interface
    // Contains definitions for a group of related functionalities
    // The interface's members are implemented by any class or struct that implements the interface
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
    }
}
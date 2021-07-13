using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;
using System;
using System.Linq;
using Catalog.Dtos;

namespace Catalog.Controllers

{
    // We need to mark this class as an ApiController
    [ApiController]
    // Need to define the route of the url. Defines the http route
    [Route("[controller]")]
    // Controllerclasses should be enabled - inherited class
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // * ----- GETTING ALL THE ITEMS ----- *
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select( item => item.AsDto());
            return items;
        }

        // * ----- GETTING ONE ITEM ----- *
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        // * ----- POSTING AN ITEM ----- *
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTime.UtcNow
            };

            repository.CreateItem(item);

            // Header for specifying the location of more information of the current item.
            return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
        }


        // * ----- CHANGING AN ITEM ----- *
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with 
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repository.UpdateItem(updatedItem);

            return NoContent();

        }


        // * ----- DELETING AN ITEM ----- *
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound(); 
            }

            repository.DeleteItem(id);

            return NoContent();
        }
    }
}
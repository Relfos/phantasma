﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PhantasmaApp.Models;

using Xamarin.Forms;
using PhantasmaApp.Helpers;

[assembly: Dependency(typeof(PhantasmaApp.Services.DataStore))]
namespace PhantasmaApp.Services
{
	public class DataStore 
	{
		bool isInitialized;
		List<Item> items;

        public string filter { get; private set; }

        public DataStore(string filter)
        {
            this.filter = filter;
        }


		public async Task<bool> AddItemAsync(Item item)
		{
			await InitializeAsync();

			items.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateItemAsync(Item item)
		{
			await InitializeAsync();

			var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
			items.Remove(_item);
			items.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteItemAsync(Item item)
		{
			await InitializeAsync();

			var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
			items.Remove(_item);

			return await Task.FromResult(true);
		}

		public async Task<Item> GetItemAsync(string id)
		{
			await InitializeAsync();

			return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
		{
			await InitializeAsync();

			return await Task.FromResult(items);
		}

		public Task<bool> PullLatestAsync()
		{
			return Task.FromResult(true);
		}


		public Task<bool> SyncAsync()
		{
			return Task.FromResult(true);
		}

		public async Task InitializeAsync()
		{
			if (isInitialized)
				return;

            //Utility.Breakpoint();

            items = new List<Item>();
            List<Item> _items;

            switch (filter)
            {
                case "Inbox":
                    {
                        _items = new List<Item>
                            {
                                new Item { Id = Guid.NewGuid().ToString(), Author = "Cat man", Title="Something about cats", Content="The cats are hungry", Time = "Now"},
                                new Item { Id = Guid.NewGuid().ToString(), Author = "Tonny Mandella", Title="Learn F#", Content="Seems like a functional idea", Time = "Today, 12pm"},
                                new Item { Id = Guid.NewGuid().ToString(), Author = "Billy Go", Title="About last gig", Content="Hey man, learn to play guitar!", Time = "Today, 14pm"},
                                new Item { Id = Guid.NewGuid().ToString(), Author = "Mother Lei", Title="Buy some new candles", Content="Pine and cranberry for that winter feel", Time = "Yesterday, 14pm"},
                                new Item { Id = Guid.NewGuid().ToString(), Author = "Santa Claus", Title="Complete holiday shopping", Content="Keep it a secret!", Time = "Friday, 17pm"},
                                new Item { Id = Guid.NewGuid().ToString(), Author = "Yu Dev", Title="Finish a todo list", Content="Done", Time = "Wednesday, 15pm"},
                            };
                        break;
                    }

                case "Sent":
                    {
                        _items = new List<Item>
                            {
                                new Item { Id = Guid.NewGuid().ToString(), Author = "Dog man", Title="Something about dogs", Content="The dogs are hungry too", Time = "Now"},
                            };
                        break;
                    }
                default: return;
            }

            foreach (Item item in _items)
			{
				items.Add(item);
			}

			isInitialized = true;
		}
	}
}

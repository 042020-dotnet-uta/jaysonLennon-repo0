using System;
using System.Linq;
using System.Collections.Generic;
using Util;
using StoreExtensions;
using StoreDb;

namespace StoreCliMenuUser
{
    class SelectLocation : CliMenu, IMenu
    {
        public SelectLocation(ApplicationData.State appState): base(appState) { }
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Select store");
        }

        public void InputLoop()
        {
                Console.WriteLine("Select store for orders:");

                var numLocations = 0;
                IEnumerable<Guid> locationIds = null;
                using (var db = new StoreContext(this.ApplicationState.DbOptions))
                {
                    var locations = db.GetLocations();
                    var locationsIter = Enumerable.Range(1, locations.Count()).Zip(locations);
                    foreach(var location in locationsIter)
                    {
                        Console.WriteLine($"  {location.First}. {location.Second.Name}");
                    }
                    numLocations = locationsIter.Count();
                    locationIds = locationsIter.Select(i => i.Second.LocationId).ToList();
                }

                bool validator(int num) {
                    if (num > 0 && num <= numLocations)
                    {
                        return true;
                    } else {
                        CliPrinter.Error($"Please enter a store number between 1 and {numLocations}");
                        return false;
                    }
                }

                var locationSelected = CliInput.GetInt(0, validator, "Enter store number:");

                var customerId = this.ApplicationState.CustomerId;
                var locationId = locationIds.ElementAt(locationSelected - 1 ?? 0);

                if (this.ApplicationState.DbOptions.SetDefaultLocation(customerId, locationId))
                {
                    CliInput.PressAnyKey("Default store set.");
                    this.ApplicationState.OperatingLocationId = locationId;
                    this.MenuExit();
                    return;
                } else {
                    CliInput.PressAnyKey("An error occurred while setting the default store. Please try again.");
                }
        }
    }
}

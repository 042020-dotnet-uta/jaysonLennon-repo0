using System;
using System.Linq;
using System.Collections.Generic;
using StoreCliUtil;
using StoreExtensions;
using StoreDb;

namespace StoreCliMenuUser
{
    /// <summary>
    /// Menu to set a default location.
    /// </summary>
    class SelectLocation : CliMenu, IMenu
    {
        /// <summary>
        /// Create this menu.
        /// </summary>
        /// <param name="appState">Global application state.</param>
        /// <returns>This menu.</returns>
        public SelectLocation(ApplicationData.State appState): base(appState) { }

        /// <summary>
        /// Print this menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            CliPrinter.Title("Select store");
        }

        /// <summary>
        /// Handle menu input.
        /// </summary>
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

                var customerId = this.ApplicationState.UserData.CustomerId;
                var locationId = locationIds.ElementAt(locationSelected - 1 ?? 0);

                if (this.ApplicationState.DbOptions.SetDefaultLocation(customerId, locationId))
                {
                    CliInput.PressAnyKey("Store set.");
                    this.ApplicationState.UserData.OperatingLocationId = locationId;
                    this.ApplicationState.UserData.RefreshDefaultLocation();
                    this.MenuExit();
                    return;
                } else {
                    CliInput.PressAnyKey("An error occurred while setting the store. Please try again.");
                }
        }
    }
}

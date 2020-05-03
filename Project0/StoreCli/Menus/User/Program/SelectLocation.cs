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
            CliPrinter.Title("Select Location");
        }

        public void InputLoop()
        {
            Console.WriteLine("Select location for orders:");

            var numLocations = 0;
            using (var db = new StoreContext(this.ApplicationState.DbOptions))
            {
                var locations = db.GetLocations();
                var locationsIter = Enumerable.Range(1, locations.Count()).Zip(locations);
                foreach(var location in locationsIter)
                {
                    Console.WriteLine($"  {location.First}. {location.Second.Name}");
                }
                numLocations = locationsIter.Count();
            }

            bool validator(int num) {
                if (num > 0 && num <= numLocations)
                {
                    return true;
                } else {
                    CliPrinter.Error($"Please enter a location number between 1 and {numLocations}");
                    return false;
                }
            }

            var storeSelected = CliInput.GetInt(0, validator, "Enter store number:");

        }
    }
}

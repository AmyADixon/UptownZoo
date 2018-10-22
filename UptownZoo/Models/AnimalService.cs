using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptownZoo.Models {
    public static class AnimalService {
        /// <summary>
        /// Returns all animals from the DB sorted by species in ascending order
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IEnumerable<Animals> GetAllAnimals(ApplicationDbContext db) { // Taken in as a parameter to reduce coupling ("new is glue")
            return (from a in db.Animals
                        orderby a.Species
                        select a).ToList();

            // OR
            //return db.Animals.OrderBy(a => a.Name).ToList();
        }
    }
}
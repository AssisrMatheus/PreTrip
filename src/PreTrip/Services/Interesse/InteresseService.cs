﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using PreTrip.Model.Context;
using PreTrip.Model.Classes;

namespace PreTrip.Services.Interesse
{
    public class InteresseService
    {
        public void InsertOrUpdate(IEnumerable<Model.Classes.Interesse> interesses)
        {
            using (var db = new PreTripDB())
            {
                interesses.ToList().ForEach(x =>
                {
                    db.Interesses.AddOrUpdate(x);                    
                });

                db.SaveChanges();
            }
        }
    }
}
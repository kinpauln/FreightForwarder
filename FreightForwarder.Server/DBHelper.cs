using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using FreightForwarder.Domain.Entities;
using FreightForwarder.Data;
using System.Data.Entity.Validation;

namespace FreightForwarder.Server
{
    public class DBHelper
    {
        private static string _dbConnectionString = Server.Properties.Settings.Default.FFDBContext;

        #region Entry

        public static List<UserProfile> GetEntries()
        {
            try
            {
                using (FFDBContext db = new FFDBContext())
                {
                    return db.UserProfiles.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region UserProfile

        public static bool AddUserProfile(UserProfile entity)
        {
            try
            {
                using (FFDBContext db = new FFDBContext())
                {
                    if (!db.UserProfiles.Where(u => u.Email.Trim().ToLower().Equals(entity.Email.Trim().ToLower())).Any())
                    {
                        db.UserProfiles.Add(entity);
                        db.SaveChanges();
                        return true;
                    } 
                    return false;
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static UserProfile GetOneUserProfile(string email)
        {
            try
            {
                using (FFDBContext db = new FFDBContext())
                {
                    return db.UserProfiles.FirstOrDefault(uf => uf.Email.Trim().ToLower() == email.Trim().ToLower());
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
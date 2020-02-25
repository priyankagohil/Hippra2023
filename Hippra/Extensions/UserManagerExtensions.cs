using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Hippra.Models.SQL;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Hippra.Models.Enums;

namespace Hippra.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByPublicIDAsync(this UserManager<AppUser> um, int profileId)
        {
            return await um?.Users?.SingleOrDefaultAsync(x => x.PublicId == profileId);
        }
        public static async Task<AppUser> FindByPublicIDNoTrackAsync(this UserManager<AppUser> um, int profileId)
        {
            return await um?.Users?.AsNoTracking().SingleOrDefaultAsync(x => x.PublicId == profileId);
        }
        public static async Task<AppUser> FindByEmail(this UserManager<AppUser> um, string email)
        {
            return await um?.Users?.FirstOrDefaultAsync(x => x.NormalizedUserName == email.ToUpper());
        }

        public static async Task UpdateUserProfile(this UserManager<AppUser> um, ClaimsPrincipal cpUser, AppUser usr)
        {
            var user = await um.GetUserAsync(cpUser);


            user.FirstName = usr.FirstName;
            user.LastName = usr.LastName;
            user.NPIN = usr.NPIN;
            user.MedicalSpecialty = usr.MedicalSpecialty;
            user.AmericanBoardCertified = usr.AmericanBoardCertified;

            user.ResidencyHospital = usr.ResidencyHospital;
            user.MedicalSchoolAttended = usr.MedicalSchoolAttended;
            user.EducationDegree = usr.EducationDegree;
            user.Address = usr.Address;
            user.Zipcode = usr.Zipcode;
            user.State = usr.State;
            user.City = usr.City;
            user.PhoneNumber = usr.PhoneNumber; // check this 

            // save
            await um.UpdateAsync(user);
            return;
        }

        public static async Task UpdateUserOnlineStatus(this UserManager<AppUser> um, ClaimsPrincipal cpUser, UserOnlineStatus status)
        {
            var user = await um.GetUserAsync(cpUser);
            user.Status = status;
            //save
            await um.UpdateAsync(user);
            return;
        }

        public static async Task<int> GetLastPID(this UserManager<AppUser> um)
        {
            //var largest =  await um?.Users?.AsNoTracking().Select(x=>x);
            var lastItem = await um?.Users.AsNoTracking().OrderByDescending(x=>x.PublicId).FirstOrDefaultAsync();

            if (lastItem == null)
            {
                return 0;
            }
            else
            {
                return lastItem.PublicId;
            }
        }
        public static async Task<bool> ValidateAccountApproval(this UserManager<AppUser> um, string emailAccount)
        {
            var account = await um?.Users.AsNoTracking().FirstOrDefaultAsync(x => x.NormalizedEmail == emailAccount.ToUpper());
            if (account == null)
            {
                return false;
            }
            else
            {
                return account.isApproved;
            }
        }
        public static async Task<List<AppUser>> GetNotApprovedUsers(this UserManager<AppUser> um)
        {
            return await um?.Users.Where(x => x.isApproved == false).ToListAsync();
        }

        public static async Task ApproveAccount(this UserManager<AppUser> um, int PublicID)
        {
            var usr = await um?.Users?.SingleOrDefaultAsync(x => x.PublicId == PublicID);
            if (usr != null)
            {
                usr.isApproved = true;
                await um.UpdateAsync(usr);
            }

        }
        public static async Task BanAccount(this UserManager<AppUser> um, int PublicID)
        {
            var usr = await um?.Users?.SingleOrDefaultAsync(x => x.PublicId == PublicID);
            if (usr != null)
            {
                usr.isApproved = false;
                await um.UpdateAsync(usr);
            }

        }
        public static async Task DelAccount(this UserManager<AppUser> um, int PublicID)
        {
            var usr = await um?.Users?.SingleOrDefaultAsync(x => x.PublicId == PublicID);
            if (usr != null)
            {
                await um.DeleteAsync(usr);
            }

        }
    }
}

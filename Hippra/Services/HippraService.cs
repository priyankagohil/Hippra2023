using Hippra.Data;
using Hippra.Extensions;
using Hippra.Models.POCO;
using Hippra.Models.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hippra.Models.FTDesign;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http.Extensions;
using System.Security.Claims;
using Newtonsoft.Json;
using FTEmailService;
using Microsoft.EntityFrameworkCore;
using Hippra.Models.Enums;
using Hippra.Code;
using System.IO;

namespace Hippra.Services
{
    public class HippraService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ApplicationDbContext _context;
        private AppSettings AppSettings { get; set; }

        private AzureStorage Storage;
        private ImageHelper ImageHelper;
        public HippraService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext context,
            IOptions<AppSettings> settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            AppSettings = settings?.Value;
            _context = context;
            Storage = new AzureStorage(settings);
            ImageHelper = new ImageHelper(Storage);
        }
        public async Task<int> GetCaseCount()
        {
            return _context.Cases.Count();
        }
        public async Task<int> GetMyCaseCount(int profileId)
        {
            return _context.Cases.Where(s=>s.PosterID == profileId).Count();
        }
        public async Task<List<Case>> GetCases(int CurrentPage, int PageSize, int id)
        {
            List<Case> cases;
            if (id == -1)
            {
                cases = await _context.Cases.OrderByDescending(s => s.DateCreated).Include(c => c.Comments).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
            }
            else
            {
                cases = await _context.Cases.Where(u => u.PosterID == id).OrderByDescending(s => s.DateCreated).Include(c => c.Comments).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
            }
            return cases;
        }
        public async Task<SearchResultModel> GetCasesNoTracking(string searchString, bool showClosed, int SubCategory, int CurrentPage, int PageSize, int id)
        {
            List<Case> cases;
            int count = 0;
            //cases = await _context.Cases.ToListAsync();

            if (SubCategory == -1)
            {
                if (showClosed)
                {
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        if (id == -1)
                        {
                            cases = await _context.Cases.AsNoTracking().Where(s => s.Topic.Contains(searchString)).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(s => s.Topic.Contains(searchString));
                        }
                        else
                        {
                            cases = await _context.Cases.AsNoTracking().Where(s => s.Topic.Contains(searchString) && s.PosterID == id).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(s => s.Topic.Contains(searchString) && s.PosterID == id);
                        }
                    }
                    else
                    {
                        if (id == -1)
                        {
                            cases = await _context.Cases.AsNoTracking().OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync();
                        }
                        else
                        {
                            cases = await _context.Cases.AsNoTracking().Where(u => u.PosterID == id).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(u => u.PosterID == id);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        if (id == -1)
                        {
                            cases = await _context.Cases.AsNoTracking().Where(s => s.Topic.Contains(searchString) && s.Status).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(s => s.Topic.Contains(searchString) && s.Status);
                        }
                        else
                        {
                            cases = await _context.Cases.AsNoTracking().Where(s => s.Topic.Contains(searchString) && s.Status && s.PosterID == id).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(s => s.Topic.Contains(searchString) && s.Status && s.PosterID == id);
                        }
                    }
                    else
                    {
                        if (id == -1)
                        {
                            cases = await _context.Cases.AsNoTracking().Where(u => u.Status).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(u => u.Status);
                        }
                        else
                        {
                            cases = await _context.Cases.AsNoTracking().Where(u => u.Status && u.PosterID == id).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(u => u.Status && u.PosterID == id);
                        }
                    }
                }
            }
            else
            {
                if (showClosed)
                {
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        if (id == -1)
                        {
                            cases = await _context.Cases.AsNoTracking().Where(s => s.MedicalCategory == SubCategory && s.Topic.Contains(searchString)).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(s => s.MedicalCategory == SubCategory && s.Topic.Contains(searchString));
                        }
                        else
                        {
                            cases = await _context.Cases.AsNoTracking().Where(s => s.MedicalCategory == SubCategory && s.Topic.Contains(searchString) && s.PosterID == id).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(s => s.MedicalCategory == SubCategory && s.Topic.Contains(searchString) && s.PosterID == id);
                        }
                    }
                    else
                    {
                        if (id == -1)
                        {
                            cases = await _context.Cases.AsNoTracking().Where(s=> s.MedicalCategory == SubCategory).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(s => s.MedicalCategory == SubCategory);
                        }
                        else
                        {
                            cases = await _context.Cases.AsNoTracking().Where(u => u.MedicalCategory == SubCategory && u.PosterID == id).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(u => u.MedicalCategory == SubCategory && u.PosterID == id);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        if (id == -1)
                        {
                            cases = await _context.Cases.AsNoTracking().Where(s => s.MedicalCategory == SubCategory && s.Topic.Contains(searchString) && s.Status).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(s => s.MedicalCategory == SubCategory && s.Topic.Contains(searchString) && s.Status);
                        }
                        else
                        {
                            cases = await _context.Cases.AsNoTracking().Where(s => s.MedicalCategory == SubCategory && s.Topic.Contains(searchString) && s.Status && s.PosterID == id).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(s => s.MedicalCategory == SubCategory && s.Topic.Contains(searchString) && s.Status && s.PosterID == id);
                        }
                    }
                    else
                    {
                        if (id == -1)
                        {
                            cases = await _context.Cases.AsNoTracking().Where(u => u.MedicalCategory == SubCategory && u.Status).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(u => u.MedicalCategory == SubCategory && u.Status);
                        }
                        else
                        {
                            cases = await _context.Cases.AsNoTracking().Where(u => u.MedicalCategory == SubCategory && u.Status && u.PosterID == id).OrderByDescending(s => s.DateCreated).Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToListAsync();
                            count = await _context.Cases.AsNoTracking().CountAsync(u => u.MedicalCategory == SubCategory && u.Status && u.PosterID == id);
                        }
                    }
                }
            }

            SearchResultModel result = new SearchResultModel();
            result.TotalCount = count;
            result.Cases = cases;

            return result;
        }

        public async Task<Case> GetCase(int caseCaseId)
        {
            return await _context.Cases.FirstOrDefaultAsync(c => c.ID == caseCaseId);
        }
        public async Task<Case> GetCaseNoTracking(int caseCaseId)
        {
            return await _context.Cases.AsNoTracking().FirstOrDefaultAsync(c => c.ID == caseCaseId);
        }
        public async Task<bool> AddCase(Case Case)
        {
            _context.Cases.Add(Case);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> CreateEmptyCase(int userId)
        {
            string key = $"{Guid.NewGuid().ToString()}";
            int id = -1;

            Case c = new Case();
            c.PosterID = userId;
            c.Description = key;
            _context.Cases.Add(c);
            _context.SaveChanges();

            c = await _context.Cases.FirstOrDefaultAsync(s => s.Description == key);
            if (c != null)
            {
                id = c.ID;
                c.Description = "";
                _context.Cases.Update(c);
                await _context.SaveChangesAsync();
            }
            return id;

        }


        public async Task<bool> CreateCase(Case EditedCase)
        {
            var Case = await _context.Cases.FirstOrDefaultAsync(m => m.ID == EditedCase.ID);

            if (Case == null)
            {
                return false;
            }
            Case.Status = true;
            Case.DateLastUpdated = DateTime.Now;
            Case.DateCreated = EditedCase.DateCreated;
            Case.PosterName = EditedCase.PosterName;
            Case.PosterSpecialty = EditedCase.PosterSpecialty;

            Case.Topic = EditedCase.Topic;
            Case.Description = EditedCase.Description;
            Case.ResponseNeeded = EditedCase.ResponseNeeded;
            Case.MedicalCategory = EditedCase.MedicalCategory;
            Case.PatientAge = EditedCase.PatientAge;

            Case.Gender = EditedCase.Gender;
            Case.Race = EditedCase.Race;
            Case.Ethnicity = EditedCase.Ethnicity;
            Case.LabValues = EditedCase.LabValues;
            Case.CurrentStageOfDisease = EditedCase.CurrentStageOfDisease;
            Case.imgUrl = EditedCase.imgUrl;

            Case.CurrentTreatmentAdministered = EditedCase.CurrentTreatmentAdministered;
            Case.TreatmentOutcomes = EditedCase.TreatmentOutcomes;

            try
            {
                _context.Update(Case);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(EditedCase.ID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> EditCase(Case EditedCase)
        {
            var Case = await _context.Cases.FirstOrDefaultAsync(m => m.ID == EditedCase.ID);

            if (Case == null)
            {
                return false;
            }
            if (!EditedCase.Status)
            {
                Case.DateClosed = DateTime.Now;
                Case.Status = false;
            }
            Case.DateLastUpdated = DateTime.Now;

            Case.Topic = EditedCase.Topic;
            Case.Description = EditedCase.Description;
            Case.ResponseNeeded = EditedCase.ResponseNeeded;
            Case.MedicalCategory = EditedCase.MedicalCategory;
            Case.PatientAge = EditedCase.PatientAge;

            Case.Gender = EditedCase.Gender;
            Case.Race = EditedCase.Race;
            Case.Ethnicity = EditedCase.Ethnicity;
            Case.LabValues = EditedCase.LabValues;
            Case.CurrentStageOfDisease = EditedCase.CurrentStageOfDisease;

            Case.CurrentTreatmentAdministered = EditedCase.CurrentTreatmentAdministered;
            Case.TreatmentOutcomes = EditedCase.TreatmentOutcomes;
            Case.imgUrl = EditedCase.imgUrl;
            try
            {
                _context.Update(Case);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(EditedCase.ID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public async Task<bool> CloseCase(int CaseId)
        {
            var Case = await _context.Cases.FirstOrDefaultAsync(m => m.ID == CaseId);

            if (Case == null)
            {
                return false;
            }
            Case.DateClosed = DateTime.Now;
            Case.Status = !Case.Status; // closed

            try
            {
                _context.Update(Case);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(CaseId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        private bool CaseExists(int id)
        {
            return _context.Cases.Any(e => e.ID == id);
        }
        public async Task<bool> DeleteCase(int caseCaseId)
        {
            var Case = await _context.Cases.FindAsync(caseCaseId);

            if (Case != null)
            {
                _context.Cases.Remove(Case);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // comments
        public async Task<List<CaseComment>> GetComments(int caseId)
        {
            return await _context.CaseComments.Where(c => c.CaseID== caseId).ToListAsync();
        }
        public async Task<List<CaseComment>> GetCommentsNoTracking(int caseId)
        {
            return await _context.CaseComments.AsNoTracking().Where(c => c.CaseID == caseId).ToListAsync();
        }
        public async Task<CaseComment> GetComment(int caseCommentId)
        {
            return await _context.CaseComments.FirstOrDefaultAsync(c => c.ID == caseCommentId);
        }
        public async Task<CaseComment> GetCommentNoTracking(int caseCommentId)
        {
            return await _context.CaseComments.AsNoTracking().FirstOrDefaultAsync(c => c.ID == caseCommentId);
        }
        public async Task<bool> AddComment(CaseComment CaseComment)
        {
            CaseComment.ID = 0;
            _context.CaseComments.Add(CaseComment);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> EditComment(CaseComment EditedCaseComment)
        {
            var CaseComment = await _context.CaseComments.FirstOrDefaultAsync(m => m.ID == EditedCaseComment.ID);

            if (CaseComment == null)
            {
                return false;
            }
            CaseComment.Comment = EditedCaseComment.Comment;
            CaseComment.LastUpdatedDate = DateTime.Now;

            try
            {
                _context.Update(CaseComment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseCommentExists(EditedCaseComment.ID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        private bool CaseCommentExists(int id)
        {
            return _context.CaseComments.Any(e => e.ID == id);
        }


        public async Task<bool> DeleteComment(int caseCommentId)
        {
            var CaseComment = await _context.CaseComments.FindAsync(caseCommentId);

            if (CaseComment != null)
            {
                _context.CaseComments.Remove(CaseComment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // image upload
        public async Task<string> UploadImgToAzureAsync(Stream fileStream, string fileName)
        {
            return await ImageHelper.UploadImageToStorage(fileStream, fileName);
        }

        public async Task<bool> DeleteImage(string filename)
        {
            await ImageHelper.DeleteImageToStorage(filename);
            return true;
        }
        public string GetImgStorageUrl()
        {
            return AppSettings.StorageUrl;
        }
    }
}

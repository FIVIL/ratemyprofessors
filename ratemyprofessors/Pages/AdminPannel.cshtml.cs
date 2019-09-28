using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages
{
    public class AdminPannelModel : PageModel
    {
        private readonly DataBaseContext context;
        private readonly IConfiguration configuration;
        public AdminPannelModel(DataBaseContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
            if (LastUpdate == null) LastUpdate = DateTime.Now;
            else
            {
                if (LastUpdate.AddHours(2) < DateTime.Now)
                {
                    AdminTokens.Clear();
                    LastUpdate = DateTime.Now;
                }
            }
        }

        [BindProperty]
        public bool LogedIn { get; set; }

        [BindProperty]
        public bool SuperAdmin { get; set; }

        [BindProperty, Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [BindProperty, Display(Name = "کلمه عبور")]
        public string PassWord { get; set; }

        [BindProperty]
        public string WrongUserPass { get; set; }

        [BindProperty]
        public string Loginner { get; set; }

        public void OnGet()
        {
            var Cookie = Request.Cookies["Admin"];
            if (!Guid.TryParse(Cookie, out var Tok))
            {
                LogedIn = false;
                return;
            }
            if (!AdminTokens.Keys.Contains(Tok))
            {
                LogedIn = false;
                return;
            }           
            LogedIn = true;
            Loginner = AdminTokens[Tok];
            if (Loginner == "Hamed") SuperAdmin = true;
            else SuperAdmin = false;
        }
        private const string SuperUserName = "FIVIL";
        private const string SuperPass = "rafcvqxb2travianirx5traviancomx2";
        public void OnPost()
        {
            if (UserName == SuperUserName && PassWord == SuperPass)
            {
                SuperAdmin = true;
                LogedIn = true;
                if (!AdminTokens.Values.Contains("Hamed"))
                {
                    Guid g = Guid.NewGuid();
                    AdminTokens.Add(g, "Hamed");
                    SetCookie("Admin", g.ToString());
                }
                else
                {
                    SetCookie("Admin", AdminTokens.FirstOrDefault(x => x.Value == "Hamed").Key.ToString());
                }
                Loginner = "Hamed";
                return;
            }
            var ad = context.Accounts
                .AsNoTracking()
                .FirstOrDefault(x => x.UserName == UserName && x.PassWord == PassWord);
            if (ad != null && ad.ISAdmin)
            {
                SuperAdmin = false;
                LogedIn = true;
                if (!AdminTokens.Values.Contains(UserName))
                {
                    Guid g = Guid.NewGuid();
                    AdminTokens.Add(g, UserName);
                    SetCookie("Admin", g.ToString());
                }
                else
                {
                    SetCookie("Admin", AdminTokens.FirstOrDefault(x => x.Value == UserName).Key.ToString());
                }
                Loginner = UserName;
                return;
            }
            LogedIn = false;
            SuperAdmin = false;
            WrongUserPass = "نام کاربری یا کلمه عبور اشتباه است.";

        }

        private void SetCookie(string key, string value)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append(key, value, option);
        }
        public static readonly Dictionary<Guid, string> AdminTokens = new Dictionary<Guid, string>();
        private static DateTime LastUpdate;
    }
}
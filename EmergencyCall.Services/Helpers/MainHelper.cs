using Microsoft.AspNetCore.Http;
using EmergencyCall.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace EmergencyCall.Services.Helpers
{
    public class MainHelper
    {
        public static string GenerateFirstLetterOfValue(string value)
        {
            string firstLetter = value.Trim()[0].ToString().ToLower();
            string defaultLetter = "a";

            switch (firstLetter)
            {
                case "ç":
                    return "c";
                case "ə":
                    return "e";
                case "ğ":
                    return "g";
                case "ı":
                    return "i";
                case "ö":
                    return "o";
                case "ş":
                    return "s";
                case "ü":
                    return "u";
                case "0":
                    return defaultLetter;
                case "1":
                    return defaultLetter;
                case "2":
                    return defaultLetter;
                case "3":
                    return defaultLetter;
                case "4":
                    return defaultLetter;
                case "5":
                    return defaultLetter;
                case "6":
                    return defaultLetter;
                case "7":
                    return defaultLetter;
                case "8":
                    return defaultLetter;
                case "9":
                    return defaultLetter;
                default:
                    return firstLetter;
            }
        }
        public static string GetMonthByDigit(string lang, int month)
        {
            if(lang.ToLower() == "az")
            {
                switch (month)
                {
                    case 1:
                        return "yanvar";
                    case 2:
                        return "fevral";
                    case 3:
                        return "mart";
                    case 4:
                        return "aprel";
                    case 5:
                        return "may";
                    case 6:
                        return "iyun";
                    case 7:
                        return "iyul";
                    case 8:
                        return "avqust";
                    case 9:
                        return "sentyabr";
                    case 10:
                        return "oktyabr";
                    case 11:
                        return "noyabr";
                    case 12:
                        return "dekabr";
                    default:
                        return "yanvar";
                }
            }
            else if(lang.ToLower() == "ru")
            {
                switch (month)
                {
                    case 1:
                        return "январь";
                    case 2:
                        return "февраль";
                    case 3:
                        return "март";
                    case 4:
                        return "апрель";
                    case 5:
                        return "май";
                    case 6:
                        return "июнь";
                    case 7:
                        return "июль";
                    case 8:
                        return "август";
                    case 9:
                        return "сентябрь";
                    case 10:
                        return "октябрь";
                    case 11:
                        return "ноябрь";
                    case 12:
                        return "декабрь";
                    default:
                        return "январь";
                }
            }
            else
            {
                switch (month)
                {
                    case 1:
                        return "january";
                    case 2:
                        return "february";
                    case 3:
                        return "march";
                    case 4:
                        return "april";
                    case 5:
                        return "may";
                    case 6:
                        return "june";
                    case 7:
                        return "july";
                    case 8:
                        return "august";
                    case 9:
                        return "september";
                    case 10:
                        return "october";
                    case 11:
                        return "november";
                    case 12:
                        return "december";
                    default:
                        return "january";
                }
            }
        }
        public static ClaimsPrincipal GetClaimsPrincipal(User user, string roleName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Date", user.RecordedAtDate.ToString()),
                new Claim("IsActive", user.IsActive.ToString()),
                new Claim(ClaimTypes.Role, roleName),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "EmergencyCall");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            return principal;
        }
        public static void TryToSetLang(IHttpContextAccessor _httpContextAccessor)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["lang"] == null)
            {
                CookieOptions option = new CookieOptions();
                option.IsEssential = true;
                option.Expires = DateTime.Now.AddDays(30);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("lang", "az", option);
            }
        }
        public static void SetLang(IHttpContextAccessor _httpContextAccessor, string _lang = "az")
        {
            CookieOptions option = new CookieOptions();
            option.IsEssential = true;
            option.HttpOnly = true;
            option.Expires = DateTime.Now.AddDays(30);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("lang", _lang, option);
        }
    }
}

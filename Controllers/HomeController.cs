using LivePollingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;

namespace LivePollingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegistration([FromBody] RegisterDto model)
        {
            if (model.RoleId<=0)
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Please select Role.",
                    statusCode: 400
                ));
            }
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Please enter UserName.",
                    statusCode: 400
                ));
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Please enter Email.",
                    statusCode: 400
                ));
            }
            if (!Validator.IsValidEmail(model.Email))
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Please enter valid Email.",
                    statusCode: 400
                ));
            }
            if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.ConfirmPassword))
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Password and Confirm Password are required.",
                    statusCode: 400
                ));
            }
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Password and Confirm Password should be same.",
                    statusCode: 400
                ));
            }
            if (!Validator.IsStrongPassword(model.Password))
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Password should be as Minimum 8 characters, at least one uppercase, one lowercase, one number, one special character.",
                    statusCode: 400
                ));
            }
            if (!Validator.IsStrongPassword(model.ConfirmPassword))
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Confirm Password should be as Minimum 8 characters, at least one uppercase, one lowercase, one number, one special character.",
                    statusCode: 400
                ));
            }
            try
            {
                Registration obj = new Registration();
                DataTable dt=obj.UserRegistration(model);
                if (dt.Rows.Count>0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["Success"].ToString()))
                    {
                        return Ok(new CommonResponse<DataTable>(
                            data: null,
                            message: dt.Rows[0]["Message"].ToString()??"",
                            statusCode: 200,
                            success: true
                        ));
                    }
                    else
                    {
                        return StatusCode(500, new CommonResponse<string>(
                            message: dt.Rows[0]["Message"].ToString()??"Something went wrong.!",
                            statusCode: 500
                        ));
                    }
                }
                else
                {
                    return StatusCode(500, new CommonResponse<string>(
                        message:"Something went wrong.!",
                        statusCode: 500
                    ));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CommonResponse<string>(
                    message: $"Server error: {ex.Message}",
                    statusCode: 500
                ));
            }
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin([FromBody] Login model)
        {
            if (model.RoleId <= 0)
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Please select Role.",
                    statusCode: 400
                ));
            }
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                return BadRequest(new CommonResponse<string>(
                    message: "Please enter UserName.",
                    statusCode: 400
                ));
            }
            try
            {
                Login obj = new Login();
                DataTable dt = obj.userLogin(model);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["Success"].ToString()))
                    {
                        return Ok(new CommonResponse<DataTable>(
                            data: null,
                            message: dt.Rows[0]["Message"].ToString() ?? "",
                            statusCode: 200,
                            success: true
                        ));
                    }
                    else
                    {
                        return StatusCode(500, new CommonResponse<string>(
                            message: dt.Rows[0]["Message"].ToString() ?? "Invalid Credential.!!!",
                            statusCode: 500
                        ));
                    }
                }
                else
                {
                    return StatusCode(500, new CommonResponse<string>(
                        message: "Invalid Credential.!!!",
                        statusCode: 500
                    ));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CommonResponse<string>(
                    message: $"Server error: {ex.Message}",
                    statusCode: 500
                ));
            }
        }
        //public IActionResult Login()
        //{
        //    return View();
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

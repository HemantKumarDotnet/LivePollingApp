using LivePollingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace LivePollingApp.Controllers
{
    public class PollingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PollingSchedule()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitPollingDate([FromBody] PollingDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.PollingDate))
                {
                    return BadRequest(new CommonResponse<string>(
                        message: "Please enter PollingDate.",
                        statusCode: (int)HttpStatusCode.BadRequest
                    ));
                }
                else if (!Validator.IsValidDate(model.PollingDate))
                {
                    return BadRequest(new CommonResponse<string>(
                        message: "Please enter valid PollingDate.",
                        statusCode: (int)HttpStatusCode.BadRequest
                    ));
                }
                else
                {

                    Polling obj = new Polling();
                    DataTable dt = obj.SubmitPollingDate(model);
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
                                message: dt.Rows[0]["Message"].ToString() ?? "Something went wrong.!!!",
                                statusCode: 500
                            ));
                        }
                    }
                    else
                    {
                        return StatusCode(500, new CommonResponse<string>(
                            message: "Something went wrong.!!!",
                            statusCode: 500
                        ));
                    }
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
        public IActionResult CandidateApproval()
        {
            return View();
        }
    }
}

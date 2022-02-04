using michealogundero.EmailSettings;
using michealogundero.Formatting;
using michealogundero.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace michealogundero.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IHostingEnvironment _env;
        IMailSender _mailSender;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env, IMailSender mailSender)
        {
            _logger = logger;
            _env = env;
            _mailSender = mailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(SendEmailModel model)
        {
            var responses = ApiResponse.GetApiResponseMessages();

            try
            {
                var data = await _mailSender.SendEmail(model.Name, model.Message, model.Subject, model.Email);
                return Json(new DataResult { StatusCode = responses[ApiResponse.ApiResponseStatus.Successful], Message = ApiResponse.ApiResponseStatus.Successful.ToString(), Data = data });

            }
            catch (Exception ex)
            {
                return Json(new DataResult { StatusCode = responses[ApiResponse.ApiResponseStatus.Failed], Message = ex.Message, Data = string.Empty });

            }

        }

        [HttpGet]
        public FileResult DownloadResumePdf()
        {
            try
            {
           
                string filePath = "~/documents/ogundero_micheal_ayodeji_CV.pdf";
                Response.Headers.Add("Content-Disposition", "inline; filename=ogundero_micheal_resume.pdf");
                return File(filePath, "application/pdf");
            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpGet]
        public FileResult DownloadResumeDocx()
        {
            try
            {
                
                string filePath = "~/documents/ogundero_micheal_ayodeji.docx";
                Response.Headers.Add("Content-Disposition", "inline; filename=ogundero_micheal_resume.docx");
                return File(filePath, "application/docx");
            }
            catch (Exception ex)
            {

                throw;
            }


        }
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
